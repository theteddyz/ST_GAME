using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

namespace CM_TaskSystem
{ public class Customer : Icustomer
    {
        private UnityEngine.Object prefab = Resources.Load("Assets/Prefab/Units/Customer.prefab");

       
        private static NavMeshAgent agent;


       

        public static Customer Create(Vector3 position)
        {
            if (agent == null)
            {

                agent = new NavMeshAgent();
            }

            return new Customer(position);
        }

        private Customer(Vector3 position)
        {
            
         
            
        }

        public void MoveTo(Vector3 position, Action onArrivedAtPosition = null)
        {
            
            agent.SetDestination(position);
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        agent.SetDestination(agent.pathEndPosition);
                    }
                }
            }
        }

        public bool IsMoving()
        {
            return agent.hasPath;
        }

       

    }

}
