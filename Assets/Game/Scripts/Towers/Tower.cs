using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerStats Stats;
    [SerializeField] protected Transform ShootPoint;

    protected List<Enemy> EnemiesInShootingRange;
    protected bool _isShootingAllowed;

    private SphereCollider _shootingRange;
    private BuildingPlacer _buildingPlacer;
    private Coroutine _currentAttack;

    public TowerStats TowerStats => Stats;

    public Enemy Target { get; protected set; }

    public virtual void Init(Building _)
    {
        _shootingRange.enabled = true;
        _shootingRange.radius = Stats.ShootRange;
        _isShootingAllowed = true;
        _buildingPlacer.BuildingPlaced -= Init;
    }

    private void Awake()
    {
        _shootingRange = GetComponent<SphereCollider>();
        EnemiesInShootingRange = new List<Enemy>();
        _buildingPlacer = FindObjectOfType<BuildingPlacer>();

        _shootingRange.enabled = false;
        _isShootingAllowed = false;
        _buildingPlacer.BuildingPlaced += Init;
    }

    private void Update()
    {
        if (_currentAttack == null && EnemiesInShootingRange.Count > 0)
            _currentAttack = StartCoroutine(Attack(EnemiesInShootingRange[EnemiesInShootingRange.Count - 1]));
    }

    private void Shoot(Enemy target)
    {
        var shell = Instantiate(Stats.ShellPrefab, ShootPoint.position, ShootPoint.rotation);
        shell.Init(target);
    }

    private IEnumerator Attack(Enemy target)
    {
        if (_isShootingAllowed == false)
            yield break;

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
