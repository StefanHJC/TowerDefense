
using UnityEngine;

public class BuildingStats : Stats
{
    [SerializeField] protected int BuildingPrice;

    public int Price => BuildingPrice;
}
