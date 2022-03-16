using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _lastAttackTime;

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = Enemy.AttackDelay;
        }
        _lastAttackTime += Time.deltaTime;
    }

    private void Attack(Gate target)
    {
        Animator.Play(EnemyAnimatorController.States.Attack);
        target.TakeDamage(Enemy.Damage);
    }
}
