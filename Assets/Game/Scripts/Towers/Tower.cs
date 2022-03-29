using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerStats Stats;
    [SerializeField] protected Transform ShootPoint;

    private SphereCollider _sphereCollider;

    protected List<Enemy> EnemiesInShootingRange;

    public Enemy Target { get; protected set; }
   
    public abstract IEnumerator Attack(Enemy target);

    protected virtual void Init()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = Stats.ShootRange;
    }

    protected virtual void Shoot(Transform target)
    {
        var shell = Instantiate(Stats.ShellPrefab, ShootPoint.position, ShootPoint.rotation);
        shell.SetTarget(target);
    }
}
