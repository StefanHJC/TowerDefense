using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WayPoint))]
public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<Road> _roadsFromSpawners;
    [SerializeField] private List<Road> _roadsToCity;
    [SerializeField] private float _sphereGizmosRadius;

    public IReadOnlyList<Road> RoadsToCity => _roadsToCity;

    private void Start()
    {
        ConnectRoads();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _sphereGizmosRadius);
    }

    private void ConnectRoads()
    {
        foreach (var road in _roadsFromSpawners)
            road.Way[road.Way.Count - 1].transform.position = transform.position;
    }
}
