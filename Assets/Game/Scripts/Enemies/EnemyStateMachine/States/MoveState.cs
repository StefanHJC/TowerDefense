using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private Animator _animator;
    private WayPoint _target;
    private int _targetIndex;

    public override void Enter(CityEnter city)
    {
        base.Enter(city);
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(EnemyAnimatorController.States.Run);
    }

    private void Start()
    {
        _targetIndex = 0;
        _target = GetNextWayPoint();
        _target.Reached += OnWayPointReached;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Enemy.Speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _animator.ResetTrigger(EnemyAnimatorController.States.Run);
    }

    private void OnWayPointReached()
    {
        _target.Reached -= OnWayPointReached;
        _targetIndex++;
        _target = GetNextWayPoint();
        _target.Reached += OnWayPointReached;
    }

    private WayPoint GetNextWayPoint() => Enemy.Road.Way[_targetIndex];
}
