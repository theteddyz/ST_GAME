using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CM_TaskSystem
{
    

    public class GameHandler : MonoBehaviour
    {
        [SerializeField] public GameObject customer;
        private CM_TaskSystem taskSystem;
        private void Start()
        {
            taskSystem = new CM_TaskSystem();

            Instantiate(customer);




        }
    }
    
    
    
}
