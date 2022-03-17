using UnityEngine;
using UnityEngine.Events;

public class Gate : Obstacle
{
    public int MaxHealth => Health;
    public int Health => CurrentHealth;

    public event UnityAction HealthChanged;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        HealthChanged.Invoke();
    }

    private void Start()
    {
        CurrentHealth = Health;
    }
}
