using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskSystem
{
    public class OrderChecker : MonoBehaviour
    {
        private TaskSystem taskSystem;
        public CustomerTaskAI cTask;
     

        public void Start()
        {
            cTask = GetComponent<CustomerTaskAI>();
            StartCoroutine(cTask.SeatDisable());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            
            
             
            if (col.gameObject.tag == "Player")
            {
                cTask.enabled = true;
                cTask.OrderValidator = true;
                Debug.Log("Enter");

            }
        }
    }

    }
