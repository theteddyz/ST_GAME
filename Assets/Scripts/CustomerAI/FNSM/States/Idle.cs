using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : BaseState
{
    private MovementSM _sm;
    private bool _noTask;


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
       
            stateMachine.ChangeState(_sm.nextTask);
        
        
    }
}
