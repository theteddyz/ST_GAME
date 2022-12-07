using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class MovementSM : StateMachine
{
    public static Transform _transform;
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;
    public static NavMeshAgent agent;
    public static float speed = 0.1f;

   
    
   

    

    private void Awake()
    {

        
       
        agent = GetComponent<NavMeshAgent>();
        idleState = new Idle(this);
        movingState = new Moving(this);


    }



    protected override BaseState GetInitialState()
    {
        
        return idleState;
    }


}
