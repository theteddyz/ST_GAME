using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moving : BaseState
{
    private MovementSM _sm;
    private bool _moving;


    public Moving(MovementSM stateMachine) : base("Moving", stateMachine)
    {
        _sm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _moving = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        
        Transform location = Idle.newPosition;
     
        if (_moving == true)
        {
            if (Vector2.Distance(MovementSM.agent.transform.position, location.position) < 0.01f)
            {
                
                
                _moving = false;
                stateMachine.ChangeState(_sm.idleState);

            }
            else
            {
                MovementSM.agent.SetDestination(location.position);

            }
        }

        
    }
}
