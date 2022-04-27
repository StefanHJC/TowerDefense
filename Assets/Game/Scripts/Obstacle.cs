using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public int MaxHealth => _health;
    public int Health => _currentHealth;

    public event UnityAction Destroyed;
    public event UnityAction HealthChanged;

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroyed?.Invoke();
        }
    }

    private void Start()
    {
        _currentHealth = _health;
    }
}