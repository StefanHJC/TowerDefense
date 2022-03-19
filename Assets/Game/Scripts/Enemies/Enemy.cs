using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDelay;
    [SerializeField] private CityEnter _destination;
    [SerializeField] private int _reward;
    [SerializeField] private Road _roadToCity;

    private int _currentHealth;
    private Obstacle _target;
    private Rigidbody _rigidbody;

    public int Damage => _damage;
    public float AttackDelay => _attackDelay;
    public float Speed => _speed;
    public int Health => _currentHealth;
    public Obstacle Target => _target;
    public CityEnter Destination => _destination;
    public Road Road => _roadToCity;
    public Rigidbody Rigidbody => _rigidbody;

    public event UnityAction Died;
    public event UnityAction TargetDestroyed;
    public event UnityAction<Obstacle> ObstacleReached;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Debug.Log("Die");
            Died?.Invoke();
        }
    }

    private void Start()
    {
        _currentHealth = _health;
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
    }

    private void OnObstacleDestroyed()
    {
        _target.Destroyed -= OnObstacleDestroyed;
        TargetDestroyed?.Invoke();
        _target = null;
    }
}
