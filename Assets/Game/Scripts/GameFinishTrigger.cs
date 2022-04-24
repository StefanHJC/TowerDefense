using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CityEnter))]
public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private WavesHandler _wavesHandler;
    [SerializeField] private UnityEvent<bool> _gameOver;

    private CityEnter _cityEnter;

    private void Start()
    {
        _cityEnter = GetComponent<CityEnter>();
        _cityEnter.MonsterMissed += OnMonsterMissed;
        _wavesHandler.AllWavesExpired += OnAllWavesExpired;
    }

    private void OnMonsterMissed()
    {
        if (_cityEnter.MissedMonstersCount >= _levelSettings.NeedMissForLose)
        {
            _gameOver?.Invoke(false);
            _cityEnter.MonsterMissed -= OnMonsterMissed;
        }
    }

    private void OnAllMonstersKilled()
    {
        _gameOver?.Invoke(true);
    }

    private void OnAllWavesExpired()
    {
        Debug.Log("Wave expired");
    }
}
