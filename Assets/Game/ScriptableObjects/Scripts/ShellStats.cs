using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/ShellStats", order = 51)]
public class ShellStats : Stats
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;

    public int Speed => _speed;
    public int Damage => _damage;
}
