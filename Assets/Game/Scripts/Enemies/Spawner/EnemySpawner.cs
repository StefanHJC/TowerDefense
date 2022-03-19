using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private WavesHandler _wavesHandler;

    private int _currentWaveIndex;
    private int _spawnedAmount;
    private Wave _currentWave;

    public int CurrentWaveIndex => _currentWaveIndex;
    public List<Wave> Waves => _waves;

    public event UnityAction AllEnemiesSpawned;

    [System.Serializable]
    public class Wave
    {
        public GameObject Prefab;
        public int Count;
        public float Delay;
    }

    private void Start()
    {
        _wavesHandler.WaveStarted += SpawnNextWave; 
    }

    public void SpawnNextWave()
    {
        SetWave(++_currentWaveIndex);
        _spawnedAmount = 0;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Prefab, transform.position, transform.rotation).GetComponent<Enemy>();
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
    }
}
