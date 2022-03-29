using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/TowerStats", order = 51)]
public class TowerStats : Stats
{
    [SerializeField] private float _shootRange;
    [SerializeField] private int _buildPrice;
    [SerializeField] private float _delayBetweenShoots;
    [SerializeField] private Shell _shellPrefab;

    public float ShootRange => _shootRange;
    public int BuildPrice => _buildPrice;
    public float DelayBetweenShoots => _delayBetweenShoots;
    public Shell ShellPrefab => _shellPrefab;
}
