using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
   
   private NavMeshAgent agent;
   private Animator animator;

   private float oldPositiony = 0.0f;
   private float oldPositionx = 0.0f;

   private void Start()
   {

      animator = GetComponent<Animator>();
      agent = GetComponent<NavMeshAgent>();
      agent.updateRotation = false;
      agent.updateUpAxis = false;
      oldPositiony = transform.position.y;
      
   }

   private void Update()
   {
      
      animator.SetFloat("Speed",agent.velocity.magnitude);



   }
}
