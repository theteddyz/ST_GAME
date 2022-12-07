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
        public  bool done = false;

        public bool isThere;
        public int choiceNumber;
        public int seatChoiceNumber;
        public void Setup(Icustomer customer, TaskSystem taskSystem)
        {
            this.customer = customer;
            this.taskSystem = taskSystem;
            state = State.WaitingForNextTask;
        }

        private void Update()
        {
           
            
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            
            if (done == false)
            {
                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            done = true;
                            state = State.WaitingForNextTask;

                            
                        }
                    }
                       
                }
            }
            
            
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
                   ExecuteTask_MoveToPosition(task as TaskSystem.Task.MoveToPosition);

                  

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

        private void ExecuteTask_MoveToPosition(TaskSystem.Task.MoveToPosition moveToPositiontask)
        {
            done = false;
            
            Debug.Log("ExecutingTask");
            customer.MoveTo(moveToPositiontask.targetPosition);

            



        }
        

        IEnumerator ExecuteTask_Order(TaskSystem.Task.Order orderTask)
        {
            
            done = true;
            ThinkingOrder();

            yield return new WaitForSeconds(5);
           
           SelectOrder();

           yield return new WaitForSeconds(2);
           HideOrder();





        }

        IEnumerator ExecuteTask_ChooseSeat(TaskSystem.Task.ChooseSeat chooseSeat)
        {

            
            
            seatChoiceNumber = UnityEngine.Random.Range(0, GameHandler.seats.Count);
            Debug.Log(seatChoiceNumber);
            while (GameHandler.seats[choiceNumber].isTaken = true)
            {
                yield return null;
                
                customer.MoveTo(GameHandler.seats[seatChoiceNumber].chair.transform.position);
                
                GameHandler.seats[seatChoiceNumber].isTaken = true;
                yield return new WaitForSeconds(2f);
                SittingDown();
                
                




            }
            
            
            
            
            








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
            state = State.WaitingForNextTask;
        }

        private void SittingDown()
        {
            NavMeshAgent agent = this.gameObject.GetComponent<NavMeshAgent>();
            if (agent.remainingDistance == 0)
            {
                Debug.Log("FUCKFUCKFUCK");
            }
        }
    }

    
    
}

