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
    [HideInInspector]
    public NextTask nextTask;
    public static NavMeshAgent agent;
    public static int currentWaypointIndex = 0;
    public static Transform[] _waypoints;
    public Transform[] editorWaypoints;
    public static float speed = 0.1f;

   
    
   

    

    private void Awake()
    {

        _waypoints = editorWaypoints;
        Debug.Log(_waypoints.Length);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        idleState = new Idle(this);
        movingState = new Moving(this);
        nextTask = new NextTask(this);

    }



    protected override BaseState GetInitialState()
    {
        
        return idleState;
    }


}
