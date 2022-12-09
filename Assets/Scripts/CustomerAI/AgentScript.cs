using System;
using System.Collections;
using System.Collections.Generic;
using TaskSystem;
using UnityEngine;
using UnityEngine.AI;
using TaskSystem;
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
