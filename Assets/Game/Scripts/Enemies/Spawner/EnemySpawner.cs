using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private WavesHandler _wavesHandler;
    [SerializeField] private Road _road;

    private int _currentWaveIndex;
    private int _spawnedAmount;
    private Wave _currentWave;

    public int CurrentWaveIndex => _currentWaveIndex;
    public IReadOnlyList<Wave> Waves => _waves;

    public event UnityAction AllEnemiesInWaveSpawned;
    public event UnityAction<Enemy> EnemyDied;

    [System.Serializable]
    public class Wave
    {
        public GameObject Prefab;
        public int Count;
        public float Delay;
    }

    public void SpawnNextWave()
    {
        if (_currentWaveIndex == _waves.Count - 1)
            _wavesHandler.WaveStarted -= SpawnNextWave;

        SetWave(++_currentWaveIndex - 1);
        _spawnedAmount = 0;

        if (_currentWave.Count > 0)
            StartCoroutine(SpawnWave());
    }

    private void Start()
    {
            _wavesHandler.WaveStarted += SpawnNextWave;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void OnEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Prefab, transform.position, transform.rotation).GetComponent<Enemy>();
        enemy.SetRoad(_road);
        enemy.Died += OnEnemyDied;
        _spawnedAmount++;
    }

    private IEnumerator SpawnWave()
    {
        var waitForSpawnDelay = new WaitForSeconds(_currentWave.Delay);

        while(_spawnedAmount < _currentWave.Count)
        {
            SpawnEnemy();
            yield return waitForSpawnDelay;
        }
        AllEnemiesInWaveSpawned?.Invoke();
    }
}
