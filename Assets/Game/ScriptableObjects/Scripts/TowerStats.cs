using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/TowerStats", order = 51)]
public class TowerStats : BuildingStats
{
    [SerializeField] private float _shootRange;
    [SerializeField] private float _delayBetweenShoots;
    [SerializeField] private Shell _shellPrefab;

    public float ShootRange => _shootRange;
    public float DelayBetweenShoots => _delayBetweenShoots;
    public Shell ShellPrefab => _shellPrefab;
}
