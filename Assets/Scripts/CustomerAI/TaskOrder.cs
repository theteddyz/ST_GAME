using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class TaskOrder : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    private int _currentWayPointIndex = 0;
    private float _speed = 2f;
    
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    public TaskOrder(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;

    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }

       

        }
        else
        {
            Transform wp = _waypoints[_currentWayPointIndex];
            if (Vector2.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector2.MoveTowards(_transform.position, wp.position, CustomerBT.speed * Time.deltaTime);
            }

        }

        
        state = NodeState.RUNNING;
        return state;
    }
}
