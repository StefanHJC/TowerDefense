using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTransition : Transition
{
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        NeedTransit = true;
    }
}
