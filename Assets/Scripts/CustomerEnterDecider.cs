using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaskSystem;
using Unity.VisualScripting;
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
                if (ranroll == 0 && queueManager.customerList.Count < 4 )
                {
                    StartCoroutine(GrabCustomer(col.gameObject, queueManager.entrancePosition));



                }
                else
                {
                    StartCoroutine(col.gameObject.GetComponent<CustomerTaskAI>().DestroyCustomer());
                    col.gameObject.tag = "Untagged";
                }
            }
        }

        IEnumerator GrabCustomer(GameObject customer, Vector3 position)
        {
            yield return new WaitForSeconds(2);
            NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
            agent.destination = position;
            
            
        }
        
        
    }

    

