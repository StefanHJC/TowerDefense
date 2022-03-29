using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private Road _roadToCity;

    private int _currentHealth;
    private Obstacle _target;
    private Rigidbody _rigidbody;

    public EnemyStats Stats => _stats;
    public int Health => _currentHealth;
    public Obstacle Target => _target;
    public Road Road => _roadToCity;
    public Rigidbody Rigidbody => _rigidbody;

    public event UnityAction Died;
    public event UnityAction TargetDestroyed;
    public event UnityAction RoadChanged;
    public event UnityAction<Obstacle> ObstacleReached;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Died?.Invoke();
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
