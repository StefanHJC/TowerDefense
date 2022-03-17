using UnityEngine;
using UnityEngine.Events;

public class CityEnter : MonoBehaviour
{
    private int _missedMonstersCount;

    public event UnityAction MonsterMissed;
    public int MissedMonstersCount => _missedMonstersCount;

    private void Start()
    {
        _missedMonstersCount = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy.gameObject);
            _missedMonstersCount++;
            MonsterMissed?.Invoke();
        }
    }
}
