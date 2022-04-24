using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/EnemyStats", order = 51)]
public class EnemyStats : Stats
{
    [SerializeField] private int _damage;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDelay;
    [SerializeField] private int _reward;
    [SerializeField] private float _deathSpeed;

    public int Damage => _damage;
    public int MaxHealth => _maxHealth;
    public int Reward => _reward;
    public float AttackDelay => _attackDelay;
    public float Speed => _speed;
    public float DeathSpeed => _deathSpeed;
    
}
