using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private Enemy _enemy;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _animator.SetTrigger(EnemyAnimatorController.States.Run);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _enemy.Speed * Time.deltaTime);
    }
}
