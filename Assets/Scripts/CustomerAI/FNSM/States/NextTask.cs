using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextTask : BaseState
{
    private MovementSM _sm;
    public Transform wpr;
    public static Transform nextTaskLocation;
    private bool nextTaskSelected = false;
    


    public NextTask(MovementSM stateMachine) : base("NextTask", stateMachine)
    {
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
       
        nextTaskSelected = false;
        UpdateLogic();
    }

    public override void UpdateLogic()
    {
        
        Transform wp = MovementSM._waypoints[MovementSM.currentWaypointIndex];
        nextTaskLocation = wp;
        nextTaskSelected = true;
        if (nextTaskSelected = true)
        {
            stateMachine.ChangeState(_sm.movingState);
        }
    }

    public override void UpdatePhysics()
    {
       base.UpdatePhysics(); 
    }
}
