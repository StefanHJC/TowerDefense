using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/ObstacleStats", order = 51)]
public class ObstacleStats : Stats
{
    [SerializeField] private int _buildPrice;
    [SerializeField] private int _maxHealth;

    public int BuildPrice => _buildPrice;
    public int MaxHealth => _maxHealth;
}
