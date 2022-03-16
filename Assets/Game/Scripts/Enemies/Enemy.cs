using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyStateMachine))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Gate _target;
    [SerializeField] private int _reward;

    private int _currentHealth;
    private Animator _animator;
    private EnemyStateMachine _stateMachine;
    
    public int Damage => _damage;
    public float AttackDelay => _attackDelay;
    public float Speed => _speed;
    public Gate Target => _target;
    public int Health => _currentHealth;

    public event UnityAction Died;

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
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<EnemyStateMachine>();
    }
}
