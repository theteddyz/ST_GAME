using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TaskSystem;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine.AI;


namespace TaskSystem
{
    public class CustomerTaskAI : MonoBehaviour
    {
        
        private enum State
        {
           WaitingForNextTask,
           ExecutingTask,
        }
        private Icustomer customer;
        private TaskSystem taskSystem;
        private State state;
        private float waitingTimer;
        



        public int choiceNumber;

        public void Setup(Icustomer customer, TaskSystem taskSystem)
        {
            this.customer = customer;
            this.taskSystem = taskSystem;
            state = State.WaitingForNextTask;
        }
        

        private void Update()
        {
            
            switch (state)
            {
                case State.WaitingForNextTask:
                    //Waiting to request new task
                    waitingTimer -= Time.deltaTime;
                    if (waitingTimer <= 0)
                    {
                        float waitingTimerMax = .2f;
                        waitingTimer = waitingTimerMax;
                        RequestNextTask();
                    }
                    break;
                case State.ExecutingTask:
                    break;
                    
                    
            }
        }

        private void RequestNextTask()
        {
            
            Debug.Log("Requesting Next Task");
           TaskSystem.Task task =  taskSystem.RequestNextTask();
           if (task == null)
           {
               state = State.WaitingForNextTask;
           }
           else
           {
               state = State.ExecutingTask;
               
               if (task is TaskSystem.Task.MoveToPosition)
               {
                   StartCoroutine(ExecuteTask_MoveToPosition(task as TaskSystem.Task.MoveToPosition));

                   return;
                   
               }
               if (task is TaskSystem.Task.Order)
               {
                   StartCoroutine(ExecuteTask_Order(task as TaskSystem.Task.Order));
                   return;
               }

               if (task is TaskSystem.Task.ChooseSeat)
               {
                   StartCoroutine(ExecuteTask_ChooseSeat(task as TaskSystem.Task.ChooseSeat));
                   return;
               }
           }
           
           
        }

        IEnumerator ExecuteTask_MoveToPosition(TaskSystem.Task.MoveToPosition moveToPositiontask)
        {
           
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            Debug.Log("ExecutingTask");
            customer.MoveTo(moveToPositiontask.targetPosition);
            isNotMoving();
            Debug.Log(isNotMoving());
            yield return new WaitUntil((() => isNotMoving() == true));

         
            state = State.WaitingForNextTask;
            yield return new WaitForSeconds(1);








        }


        

        IEnumerator ExecuteTask_Order(TaskSystem.Task.Order orderTask)
        {

            int selectedSeatChoice;
            ThinkingOrder();

            yield return new WaitForSeconds(5);
           
           SelectOrder();

           yield return new WaitForSeconds(2);
           HideOrder();
           
           SelectSeat(0);
           selectedSeatChoice = SelectSeat(0);
           if (selectedSeatChoice == -2)
           {
               yield break;
           }
         
           customer.MoveTo(GameHandler.seats[selectedSeatChoice].chair.transform.position);
           isNotMoving();
           yield return new WaitUntil((() => isNotMoving() == true));
           SittingDown(GameHandler.seats[selectedSeatChoice].chair.transform);
           GameHandler.seats[selectedSeatChoice].isTaken = true;
           GameHandler.seats.RemoveAt(selectedSeatChoice);

        }

        IEnumerator ExecuteTask_ChooseSeat(TaskSystem.Task.ChooseSeat chooseSeat)
        {
            int selectedSeatChoice;

            SelectSeat(0);
            selectedSeatChoice = SelectSeat(0);

            if (selectedSeatChoice == -2)
            {
                yield break;
            }
         
            customer.MoveTo(GameHandler.seats[selectedSeatChoice].chair.transform.position);
            isNotMoving();
            yield return new WaitUntil((() => isNotMoving() == true));
            SittingDown(GameHandler.seats[selectedSeatChoice].chair.transform);
            GameHandler.seats[selectedSeatChoice].isTaken = true;
            GameHandler.seats.RemoveAt(selectedSeatChoice);
        }

        private int SelectSeat(int seatChoiceNumber)
        {
            
            if (GameHandler.seats.Count == 0)
            {
                Debug.Log("all seats are taken");
                return -2;
            }
            else
            {
                 seatChoiceNumber = UnityEngine.Random.Range(0, GameHandler.seats.Count);
           
                if (GameHandler.seats[seatChoiceNumber].isTaken = true)
                {
                    seatChoiceNumber = UnityEngine.Random.Range(0, GameHandler.seats.Count);
                }

            }

            return seatChoiceNumber;

        }
        private void SittingDown(Transform chair)
        {
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            
                
            
            gameObject.transform.position = chair.transform.position ;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CustomerTaskAI>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;


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
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            

            return false;
        }

        
    }

    
    
}

