using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private Road _roadToCity;

    private Obstacle _target;
    private Rigidbody _rigidbody;
    private int _currentHealth;
    private bool _isDead;

    public EnemyStats Stats => _stats;
    public Obstacle Target => _target;
    public Road Road => _roadToCity;
    public Rigidbody Rigidbody => _rigidbody;
    public int Health => _currentHealth;

    public event UnityAction<Enemy> Died;
    public event UnityAction TargetDestroyed;
    public event UnityAction RoadChanged;
    public event UnityAction<Obstacle> ObstacleReached;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0 && _isDead == false)
        {
            _isDead = true;
            Died?.Invoke(this);
        }
    }

    public void SetRoad(Road road)
    {
        _roadToCity = road;
        RoadChanged?.Invoke();
    }

    private void Start()
    {
        _currentHealth = Stats.MaxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _isDead = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Obstacle obstacle))
        {
            ObstacleReached?.Invoke(obstacle);
            obstacle.Destroyed += OnObstacleDestroyed;
            _target = obstacle;
        }
        else if (collision.TryGetComponent(out Crossroad crossroad))
        {
            var newRandomRoad = crossroad.RoadsToCity[Mathf.RoundToInt(Random.Range(0, crossroad.RoadsToCity.Count))];

            SetRoad(newRandomRoad);
        }
    }

    private void OnObstacleDestroyed()
    {
        _target.Destroyed -= OnObstacleDestroyed;
        TargetDestroyed?.Invoke();
        _target = null;
    }
}
