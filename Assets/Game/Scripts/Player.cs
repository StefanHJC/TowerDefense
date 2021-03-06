using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private LevelSettings _settings;
    [SerializeField] private WavesHandler _wavesHandler;
    [SerializeField] private BuildingPlacer _buildingPlacer;

    private int _monstersKilled;
    private int _gold;

    public int Gold => _gold;
    public int MonstersKilled => _monstersKilled;

    public event UnityAction GoldAmountChanged;

    public void AddGold(int value)
    {
        if (value < 0)
            return;

        _gold += value;
        GoldAmountChanged?.Invoke();
    }

    public void ReduceGold(int value)
    {
        if (_gold - value < 0)
            return;

        _gold -= value;
        GoldAmountChanged?.Invoke();
    }

    private void Awake()
    {
        _gold = _settings.StartGold;
        _buildingPlacer.BuildingPlaced += OnBuildingPlaced; 

        foreach (var spawner in _wavesHandler.EnemySpawners)
            spawner.EnemyDied += OnEnemyDied;
    }

    private void OnBuildingPlaced(Building building)
    {
        ReduceGold(building.Price);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        AddGold(enemy.Stats.Reward);
    }

    private void OnDisable()
    {
        _buildingPlacer.BuildingPlaced -= OnBuildingPlaced;
    }
}
