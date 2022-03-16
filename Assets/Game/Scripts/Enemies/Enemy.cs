using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Gate _target;

    private int _currentHealth;
    
    public int Damage => _damage;
    public float AttackDelay => _attackDelay;
    public float Speed => _speed;
    //public Gate Target => _target;
    public int Health => _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    private void Start()
    {
        _currentHealth = _health;
    }
}
