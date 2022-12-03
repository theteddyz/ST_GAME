using System;
using System.Collections;
using System.Collections.Generic;
using CM_TaskSystem;
using UnityEngine;
using CodeMonkey;

namespace CM_TaskSystem
{
    public class CustomerTaskAI : MonoBehaviour
    {
        private enum State
        {
           WaitingForNextTask,
           ExecutingTask,
        }
        private Icustomer customer;
        private State state;
        private float waitingTimer;
        public void Setup(Icustomer customer)
        {
            this.customer = customer;
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
                    
            }
        }

        private void RequestNextTask()
        {
            CMDebug.TextPopupMouse("RequestNextTask");
        }
    }

    
    
}

