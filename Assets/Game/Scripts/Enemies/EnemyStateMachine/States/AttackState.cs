using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    private float _lastAttackTime;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Enemy.Target);
            _lastAttackTime = Enemy.Stats.AttackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _animator.ResetTrigger(EnemyAnimatorController.States.Attack);
    }

    private void Attack(Obstacle target)
    {
        _animator.SetTrigger(EnemyAnimatorController.States.Attack);
        target.TakeDamage(Enemy.Stats.Damage);
    }
}
