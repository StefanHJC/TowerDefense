using System.Collections;
using UnityEngine;

public class DieState : State
{
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
        _enemy.transform.Translate(Vector3.down * _enemy.Stats.DeathSpeed * Time.deltaTime);
    }

    private IEnumerator Die()
    {
        while (_enemy.transform.position.y > 9)
            yield return null;

        Destroy(_enemy.gameObject);
    }
}
