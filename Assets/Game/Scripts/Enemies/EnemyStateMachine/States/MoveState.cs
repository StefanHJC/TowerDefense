using UnityEngine;

public class MoveState : State
{
    private Animator _animator;
    private WayPoint _target;
    private int _targetIndex;

    public override void Enter()
    {
        base.Enter();
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(EnemyAnimatorController.States.Run);
    }

    private void Start()
    {
        InitRoad();
        Enemy.RoadChanged += OnRoadChanged;
    }

    private void FixedUpdate()
    {
        Vector3 targetDirection = _target.transform.position - transform.position;

        Enemy.Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, _target.transform.position, Enemy.Stats.Speed * Time.fixedDeltaTime));
        Enemy.Rigidbody.MoveRotation(Quaternion.Lerp(Enemy.Rigidbody.rotation, Quaternion.LookRotation(targetDirection), 2 * Time.fixedDeltaTime));
    }

    private void OnDisable()
    {
        _animator.ResetTrigger(EnemyAnimatorController.States.Run);
    }

    private void OnWayPointReached(Enemy enemy)
    {
        if (enemy != this.Enemy)
            return;

        _target.Reached -= OnWayPointReached;
        _target = GetNextWayPoint();
        _target.Reached += OnWayPointReached;
    }

    private void OnRoadChanged()
    {
        InitRoad();
    }

    private void InitRoad()
    {
        _targetIndex = 0;
        _target = GetNextWayPoint();
        _target.Reached += OnWayPointReached;
        _animator = GetComponent<Animator>();
    }

    private WayPoint GetNextWayPoint()
    {
        var wayPoint = Enemy.Road.Way[_targetIndex];

        if (_targetIndex < Enemy.Road.Way.Count - 1)
            _targetIndex++;

        return wayPoint;
    }
}
