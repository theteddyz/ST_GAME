using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CM_TaskSystem
{
    

    public class GameHandler : MonoBehaviour
    {
        [SerializeField] public static GameObject customerPrefab;
   
        private CM_TaskSystem taskSystem;
        private void Start()
        {
            
             taskSystem = new CM_TaskSystem();
             Customer customer = Customer.Create(new Vector3(500,500));
             CustomerTaskAI customerTaskAI = customer.gameObject.AddComponent<CustomerTaskAI>();
             customerTaskAI.Setup(customer);










        }


       
    }
    
    
    
}
