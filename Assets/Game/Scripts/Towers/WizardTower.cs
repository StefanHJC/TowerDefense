using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : Tower
{
    private bool _isActive;

    public bool IsActive => _isActive;

    public override IEnumerator Attack(Enemy target)
    {
        var waitForDelay = new WaitForSeconds(DelayBetweenShoots);

        while (target.Health > 0)
        {
            Shoot(target.transform);
            yield return waitForDelay;
        }
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
    }

    private void Activate()
    {
        _isActive = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy target))
        {
            Debug.Log("Entered");
            StartCoroutine(Attack(target));
        }
    }
}
