using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;
using UnityEngine.AI;



    public class CustomerEnterDecider : MonoBehaviour
    {
        private QueueManager queueManager;
        private void OnTriggerEnter2D(Collider2D col)
        {
            NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
            
             
            if (col.gameObject.tag == "Customer")
            {
                Debug.Log("Hit first collider");
                int ranroll = UnityEngine.Random.Range(0, 2);
                if (ranroll == 0)
                {
                    agent.destination = new Vector3(3, -9, 0);

                }
                else
                {
                    StartCoroutine(col.gameObject.GetComponent<CustomerTaskAI>().DestroyCustomer());
                }
            }
        }
        
        
    }

    

