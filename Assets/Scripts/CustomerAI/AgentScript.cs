using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
   
   private NavMeshAgent agent;
   private Animator animator;



   private void Start()
   {
     
      

      animator = GetComponent<Animator>();
      agent = GetComponent<NavMeshAgent>();
      agent.updateRotation = false;
      agent.updateUpAxis = false;
      

   }

   private void Update()
   {


      animator.SetFloat("Speed",agent.velocity.magnitude);
      animator.SetFloat("Horizontal", agent.velocity.x);
      animator.SetFloat("Vertical", agent.velocity.y);

      if (agent.velocity.x == 1 || agent.velocity.x == -1 || agent.velocity.y == 1 || agent.velocity.y == -1)
      {
         animator.SetFloat("lastMoveX", agent.velocity.x);
         animator.SetFloat("lastMoveY", agent.velocity.y);
      }


   }

    private IEnumerator AnimationController()
   {
      yield return null;
      animator.SetFloat("Speed",agent.velocity.magnitude);
      animator.SetFloat("Horizontal", agent.velocity.x);
      animator.SetFloat("Vertical", agent.velocity.y);
     
      AnimationController();
   }

   
}
