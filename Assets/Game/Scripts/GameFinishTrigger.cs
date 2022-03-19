using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CityEnter))]
public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private int _needMissForLose;
    [SerializeField] private Player _player;
    [SerializeField] private WavesHandler _wavesHandler;
    [SerializeField] private UnityEvent _gameLost;
    [SerializeField] private UnityEvent _gameWon;

    private CityEnter _cityEnter;

    public int NeedMissForLose => _needMissForLose;

    private void Start()
    {
        _cityEnter = GetComponent<CityEnter>();
        _cityEnter.MonsterMissed += OnMonsterMissed;
        _wavesHandler.WaveExpired += OnWaveExpired;
    }

    private void OnMonsterMissed()
    {
        if (_cityEnter.MissedMonstersCount >= _needMissForLose)
        {
            _gameLost?.Invoke();
            _cityEnter.MonsterMissed -= OnMonsterMissed;
        }
    }

    private void OnAllMonstersKilled()
    {
        _gameWon?.Invoke();
    }

    private void OnWaveExpired()
    {
        Debug.Log("Wave expired");
    }
}
