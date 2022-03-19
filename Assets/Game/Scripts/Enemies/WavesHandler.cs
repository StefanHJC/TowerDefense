using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WavesHandler : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners;
    [SerializeField] private int _delayBetweenWaves;

    private int _totalMonstersAmount;
    private int _emptySpawnersCount;
    private float _elapsedAfterWave;

    public int TotalMonstersAmount => _totalMonstersAmount;

    public event UnityAction WaveStarted;
    public event UnityAction WaveExpired;

    public float GetTimeBeforeNextWave() => _delayBetweenWaves - _elapsedAfterWave;

    private void Start()
    {
        foreach (var spawner in _spawners)
        {
            spawner.AllEnemiesSpawned += OnAllEnemiesInSpawnerSpawned;

            foreach (var wave in spawner.Waves)
            {
                _totalMonstersAmount += wave.Count;
            }
        }
    }

    private void Update()
    {
        if (_emptySpawnersCount == _spawners.Count)
        {
            _elapsedAfterWave += Time.deltaTime;
            if (_elapsedAfterWave >= _delayBetweenWaves)
                WaveStarted?.Invoke();
        }
    }

    private void OnAllEnemiesInSpawnerSpawned()
    {
        _emptySpawnersCount++;

        foreach (var spawner in _spawners)
            if (spawner.Waves.Count - 1 == spawner.CurrentWaveIndex)
            {
                spawner.AllEnemiesSpawned -= OnAllEnemiesInSpawnerSpawned;
                _spawners.Remove(spawner);
            }
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
            spawner.AllEnemiesSpawned -= OnAllEnemiesInSpawnerSpawned;
    }
}
