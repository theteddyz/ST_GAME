using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using ForTeddy.Databases;
using Random = UnityEngine.Random;

namespace TaskSystem
{ public class Customer : Icustomer
    {

        


        public GameObject gameObject;
   
        private  NavMeshAgent unitObject;
        


        public void Awake()
        {
           
            Initialize();
            
        }

        public void Initialize()
        {
            
          
            Debug.Log("Customer initialization");
            
        }

        public static Customer Create(Vector3 position)
        {
            return new Customer(position);
        }

        private Customer(Vector3 position)
        {
            
            gameObject = GameObject.Instantiate(DatabaseManager.Instance.CustomerDatabase.GetRandomCustomer(), position, Quaternion.identity);
            
            
        }

        public void MoveTo(Vector3 position)
        {
           
            
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            
            
            agent.destination = position;

           

        }

       public  bool pathComplete()
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if ( Vector3.Distance( agent.destination, agent.transform.position) <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
 
            return false;
        }

        public bool IsMoving()
        {
            return unitObject.hasPath;
        }


      

       

    }

}