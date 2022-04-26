using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : Tower
{
    private bool _isActive;
    private Coroutine _currentAttack;


    public override IEnumerator Attack(Enemy target)
    {
        var waitForDelay = new WaitForSeconds(Stats.DelayBetweenShoots);

        while (target.Health >= 0)
        {
            Shoot(target);
            yield return waitForDelay;
        }
        EnemiesInShootingRange.Remove(target);
        _currentAttack = null;

        if (EnemiesInShootingRange.Count > 0)
            _currentAttack = StartCoroutine(Attack(EnemiesInShootingRange[EnemiesInShootingRange.Count - 1]));
    }

    private void Start()
    {
        base.Init();
        EnemiesInShootingRange = new List<Enemy>();
    }

    private void Update()
    {
        if (_currentAttack == null && EnemiesInShootingRange.Count > 0)
            _currentAttack = StartCoroutine(Attack(EnemiesInShootingRange[EnemiesInShootingRange.Count - 1]));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {
            EnemiesInShootingRange.Add(target);

            if (_currentAttack == null)
                _currentAttack = StartCoroutine(Attack(EnemiesInShootingRange[EnemiesInShootingRange.Count - 1]));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {
            EnemiesInShootingRange.Remove(target);

            if (_currentAttack != null)
            {
                StopCoroutine(_currentAttack);
                _currentAttack = null;
            }
        }
    }
}
