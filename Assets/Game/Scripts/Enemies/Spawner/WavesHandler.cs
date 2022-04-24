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

    public List<EnemySpawner> EnemySpawners => _spawners;
    public int TotalMonstersAmount => _totalMonstersAmount; 

    public event UnityAction WaveStarted;
    public event UnityAction AllWavesExpired;

    public float GetTimeBeforeNextWave() => _delayBetweenWaves - _elapsedAfterWave;

    private void Start()
    {
        foreach (var spawner in _spawners)
        {
            spawner.AllEnemiesInWaveSpawned += OnAllEnemiesInSpawnerSpawned;

            foreach (var wave in spawner.Waves)
            {
                _totalMonstersAmount += wave.Count;
            }
        }
        _emptySpawnersCount = _spawners.Count;
    }

    private void Update()
    {
        if (_emptySpawnersCount == _spawners.Count)
        {
            _elapsedAfterWave += Time.deltaTime;
            if (_elapsedAfterWave >= _delayBetweenWaves)
                StartWave();
        }
    }

    private void StartWave()
    {
        _emptySpawnersCount = 0;
        _elapsedAfterWave = 0;
        WaveStarted?.Invoke();
    }

    private void OnAllEnemiesInSpawnerSpawned()
    {
        _emptySpawnersCount++;

        for (int i = 0; i < _spawners.Count; i++)
            if (_spawners[i].Waves.Count == _spawners[i].CurrentWaveIndex)
            {
                _spawners[i].AllEnemiesInWaveSpawned -= OnAllEnemiesInSpawnerSpawned;
                _spawners[i].gameObject.SetActive(false);
                _spawners.Remove(_spawners[i]);

                if (_spawners.Count == 0)
                    AllWavesExpired?.Invoke();
            }
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
            spawner.AllEnemiesInWaveSpawned -= OnAllEnemiesInSpawnerSpawned;
    }
}
