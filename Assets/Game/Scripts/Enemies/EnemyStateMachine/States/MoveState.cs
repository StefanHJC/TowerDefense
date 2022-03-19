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

    private void FixedUpdate()
    {
        Vector3 targetDirection = _target.transform.position - transform.position;

        Enemy.Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _target.transform.position, Enemy.Speed * Time.fixedDeltaTime));
        Enemy.Rigidbody.MoveRotation(Quaternion.Lerp(Enemy.Rigidbody.rotation, Quaternion.LookRotation(targetDirection), 2 * Time.fixedDeltaTime));
    }

    private void OnDisable()
    {
        _animator.ResetTrigger(EnemyAnimatorController.States.Run);
    }

    private void OnWayPointReached()
    {
        _target.Reached -= OnWayPointReached;
        _target = GetNextWayPoint();
        _target.Reached += OnWayPointReached;
    }

    private WayPoint GetNextWayPoint()
    {
        var wayPoint = Enemy.Road.Way[_targetIndex];

        if (_targetIndex < Enemy.Road.Way.Count - 1)
            _targetIndex++;

        return wayPoint;
    }
}
