using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings/LevelSettings", order = 51)]
public class LevelSettings : ScriptableObject
{
    [SerializeField] private int _gateHealth;
    [SerializeField] private int _startGold;
    [SerializeField] private int _needMissForLose;
    [SerializeField] private int _monstersAmountMultiplier;

    public int GateHealth => _gateHealth;
    public int StartGold => _startGold;
    public int NeedMissForLose => _needMissForLose;
}
