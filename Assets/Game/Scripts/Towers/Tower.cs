using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float ShootRange;
    [SerializeField] protected int Cost;
    [SerializeField] protected float DelayBetweenShoots;
    [SerializeField] protected Shell ShellPrefab;
    [SerializeField] protected Transform ShootPoint;

    private SphereCollider _sphereCollider;

    protected List<Enemy> EnemiesInShootingRange;

    public Enemy Target { get; protected set; }
   
    public abstract IEnumerator Attack(Enemy target);

    public abstract void Build(Transform position);

    public abstract void Destroy();

    protected virtual void Init()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = ShootRange;
    }

    protected virtual void Shoot(Transform target)
    {
        var shell = Instantiate(ShellPrefab, ShootPoint.position, ShootPoint.rotation);
        shell.SetTarget(target);
    }
}
