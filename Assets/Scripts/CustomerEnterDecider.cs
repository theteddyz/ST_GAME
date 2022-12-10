using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;
using UnityEngine.AI;



    public class CustomerEnterDecider : MonoBehaviour
    {
        public QueueManager queueManager;
        public GameHandler gameHandler;
        private void OnTriggerEnter2D(Collider2D col)
        {
            NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
            
             
            if (col.gameObject.tag == "Customer")
            {
                
                int ranroll = UnityEngine.Random.Range(0, 2);
                if (ranroll == 0 && queueManager.customerList.Count < 5)
                {
                    agent.destination = queueManager.entrancePosition;

                    if (queueManager.customerList.Count == 1) 
                    {
                        gameHandler.isOrdering = true;
                    }

                }
                else
                {
                    StartCoroutine(col.gameObject.GetComponent<CustomerTaskAI>().DestroyCustomer());
                    col.gameObject.tag = "Untagged";
                }
            }
        }
        
        
    }

    

