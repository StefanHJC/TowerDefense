using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WayPoint))]
public class Crossroad : MonoBehaviour
{
    [SerializeField] private List<Road> _roadsFromSpawners;
    [SerializeField] private List<Road> _roadsToCity;
    [SerializeField] private WayPoint _wayPoint;

    public List<Road> RoadsToCity => _roadsToCity;

    public event UnityAction Reached;

    private void Start()
    {
        _wayPoint.transform.position = transform.position;
        ConnectRoads();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_wayPoint.transform.position, 30);
    }

    private void ConnectRoads(bool inspectorMode = false)
    {
        _wayPoint.transform.position = transform.position;

        foreach (var road in _roadsFromSpawners)
            road.AddWayPoint(_wayPoint);

        foreach (var road in _roadsToCity)
            road.AddWayPoint(_wayPoint, true);
    }
}
