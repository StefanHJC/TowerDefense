using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    [SerializeField] private float _deathSpeed;

    private Animator _animator;
    private Enemy _enemy;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _animator.SetTrigger(EnemyAnimatorController.States.Dead);
        StartCoroutine(Die());
    }

    private void Update()
    {
        _enemy.transform.Translate(Vector3.down * _deathSpeed * Time.deltaTime);
    }

    private IEnumerator Die()
    {
        while (_enemy.transform.position.y > -15)
            yield return null;

        Destroy(_enemy.gameObject);
    }
}
