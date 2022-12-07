using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : BaseState
{
    private MovementSM _sm;
    public static bool _noTask;
    
    public static Transform newPosition;


    public Idle(MovementSM stateMachine) : base("Idle", stateMachine)
    {
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _noTask = true;
        UpdateLogic();
    }

    public override void UpdateLogic()
    {

        if (_noTask == false)
        {
            
            stateMachine.ChangeState(_sm.movingState);
        }
        
        
    }
}
