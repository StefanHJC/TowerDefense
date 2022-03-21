using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _gold;
    private int _monstersKilled;

    public int Gold => _gold;
    public int GoldCollected => _gold;
    public int MonstersKilled => _monstersKilled;

    public event UnityAction GoldAmountChanged;

    private void AddGold(int amount)
    {
        _gold += amount;
        GoldAmountChanged?.Invoke();
    }
}
