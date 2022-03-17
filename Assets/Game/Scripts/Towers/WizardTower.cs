using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : Tower
{
    private bool _isActive;
    private Coroutine _currentAttack;

    public bool IsActive => _isActive;

    public override IEnumerator Attack(Enemy target)
    {
        var waitForDelay = new WaitForSeconds(DelayBetweenShoots);

        while (target.Health >= 0)
        {
            Shoot(target.transform);
            yield return waitForDelay;
        }
        EnemiesInShootingRange.Remove(target);
        _currentAttack = null;

        if (EnemiesInShootingRange.Count > 0)
            _currentAttack = StartCoroutine(Attack(EnemiesInShootingRange[EnemiesInShootingRange.Count - 1]));
    }

    public override void Build(Transform position)
    {
        throw new System.NotImplementedException();
    }

    public override void Destroy()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        base.Init();
        EnemiesInShootingRange = new List<Enemy>();
    }

    private void Activate()
    {
        _isActive = true;
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
}
