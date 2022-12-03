using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ordering : BaseState
{
    private MovementSM _sm;
    private bool chooseOrder;


    public Ordering(MovementSM stateMachine) : base("Ordering", stateMachine)
    {
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        chooseOrder = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        
        base.UpdatePhysics();
        
    }
}
