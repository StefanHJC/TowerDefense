using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _lastAttackTime;
    private Enemy _enemy;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _enemy.AttackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Gate target)
    {
        _animator.SetTrigger(EnemyAnimatorController.States.Attack);
        target.TakeDamage(_enemy.Damage);
    }
}
