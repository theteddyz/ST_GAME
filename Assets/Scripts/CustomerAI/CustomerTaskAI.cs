using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TaskSystem;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine.AI;
using System.Linq;



    public class CustomerTaskAI : MonoBehaviour
    {
        
        
        private State state;
        private float waitingTimer;
        public bool  OrderValidator = false;
        public int selectedSeatChoice;
        private bool allIsTaken =false;
        public int orginLocation;
        public int choiceNumber;



        private void Start()
        {
            
            
         
            
        }
        


        private void Update()
        {
            if (OrderValidator == true)
            {
                StartCoroutine(ExecuteTask_CheckOrder());
                OrderValidator = false;
            }

        }

      
        public IEnumerator ExecuteTask_MoveToPosition()
        {
           
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            isNotMoving();
            yield return new WaitUntil((() => isNotMoving() == true));

         
            
            yield return new WaitForSeconds(1);
            
        }


        

       public IEnumerator ExecuteTask_Order()
        {
           

            
            ThinkingOrder();

            yield return new WaitForSeconds(5);
           
           SelectOrder();

           yield return new WaitForSeconds(2);
           HideOrder();
           
           StartCoroutine(ExecuteTask_ChooseSeat());
           yield break;


        }

        IEnumerator ExecuteTask_ChooseSeat()
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();

            Debug.Log("Starting Coroutine");
            
          
            StartCoroutine(SelectSeat());
            
            agent.destination = (GameHandler.seats[selectedSeatChoice].chair.transform.position);
            isNotMoving();
            yield return new WaitUntil((() => isNotMoving() == true));
            SittingDown(GameHandler.seats[selectedSeatChoice].chair.transform);
            yield break;
            
            

        }

        public IEnumerator ExecuteTask_CheckOrder()
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            gameObject.tag = "Untagged";
            int ranroll = UnityEngine.Random.Range(0, 4);

            yield return new WaitForSeconds(5f);
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CustomerTaskAI>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            GameHandler.seats[selectedSeatChoice].isTaken = false;
            agent.destination =(GameHandler.waypoints[ranroll].addedWaypoint.transform.position);
            
            
            
            isNotMoving();
            yield return new WaitUntil((() => isNotMoving() == true));
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
            yield break;
            
        }

        IEnumerator SelectSeat()
        {
            
            bool isTaken = true;
            selectedSeatChoice = UnityEngine.Random.Range(0, GameHandler.seats.Count);
            do
            {
               

                selectedSeatChoice = UnityEngine.Random.Range(0, GameHandler.seats.Count);
                isTaken = GameHandler.seats[selectedSeatChoice].isTaken;
                
                


            } while (isTaken == true);
            
            yield break;
           




        }
        private void SittingDown(Transform chair)
        {
            AllIsTaken();
            gameObject.transform.position = chair.transform.position;
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (chair.GetChild(0).tag == "ChairLeft")
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CustomerTaskAI>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.AddComponent<OrderChecker>();


        }

        private void SelectOrder()
        {
            string customerChoice;
            
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            choiceNumber = UnityEngine.Random.Range(0, GameHandler.menu.Count);
            customerChoice = GameHandler.menu[choiceNumber];
            Debug.Log(customerChoice);
            if (choiceNumber == 0)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }else if (choiceNumber == 1)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }else if (choiceNumber == 2)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        private void ThinkingOrder()
        {
            Debug.Log("Selecting Order");
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }

        private void HideOrder()
        {
            if (choiceNumber == 0)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }else if (choiceNumber == 1)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }else if (choiceNumber == 2)
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
         bool isNotMoving()
        {
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            if (!agent.pathPending)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }

            return false;
        }

        private bool AllIsTaken()
        {
            for (int i = 0; i < GameHandler.seats.Count; i++)
            {
                if (GameHandler.seats[i].isTaken == false)
                {
                    allIsTaken = false;
                    return false;
                }
            }
            allIsTaken = true;

            return true;
        }
        

        public IEnumerator SeatDisable()
        {
            GameHandler.seats[selectedSeatChoice].isTaken = true;
            yield break;
        }

       public IEnumerator DestroyCustomer()
       {
           
           StartCoroutine(SetExit());
           

           StartCoroutine(VerifyExit());
           yield break;

       }

       IEnumerator SetExit()
       {
           NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
           if (orginLocation == 0 || orginLocation == 1)
           {
               int exitLocation = UnityEngine.Random.Range(2, 3);
               agent.destination = (GameHandler.waypoints[exitLocation].addedWaypoint.transform.position);
           }
           else
           {
               int exitLocation = UnityEngine.Random.Range(0, 1);
               agent.destination = (GameHandler.waypoints[exitLocation].addedWaypoint.transform.position);
           }
           
           
           yield break;
       }

       IEnumerator VerifyExit()
       {
           yield return new WaitForSeconds(15);
           Destroy(gameObject);
           yield break;
       }

        
    }


