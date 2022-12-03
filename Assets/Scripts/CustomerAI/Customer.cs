using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

namespace CM_TaskSystem
{ public class Customer : Icustomer
    {

        


        public GameObject gameObject;
        private static NavMeshAgent unitObject;


       

        public static Customer Create(Vector3 position)
        {
          
           

            return new Customer(position);
        }

        private Customer(Vector3 position)
        {
            
         
          
            
        }

        public void MoveTo(Vector3 position, Action onArrivedAtPosition = null)
        {
            
            unitObject.SetDestination(position);
            if (!unitObject.pathPending)
            {
                if (unitObject.remainingDistance <= unitObject.stoppingDistance)
                {
                    if (!unitObject.hasPath || unitObject.velocity.sqrMagnitude == 0f)
                    {
                        unitObject.SetDestination(unitObject.pathEndPosition);
                    }
                }
            }
        }

        public bool IsMoving()
        {
            return unitObject.hasPath;
        }

       

    }

}
