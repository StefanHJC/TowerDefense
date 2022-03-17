using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _gold;

    public int Gold => _gold;

    public event UnityAction GoldAmountChanged;

    private void AddGold(int amount)
    {
        _gold += amount;
        GoldAmountChanged?.Invoke();
    }

}
