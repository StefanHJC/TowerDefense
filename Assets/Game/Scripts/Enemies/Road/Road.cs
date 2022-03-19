using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _wayPoints;
    [SerializeField] private Color _colorInInspector;

    public List<WayPoint> Way => _wayPoints;

    public void AddWayPoint(WayPoint wayPoint, bool addFirst = false)
    {
        if (addFirst)
            _wayPoints.Insert(0, wayPoint);
        else
            _wayPoints.Add(wayPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _colorInInspector.a == 0 ? Color.yellow : _colorInInspector;

        for (int i = 0; i < _wayPoints.Count - 1; i++)    
        {
            Gizmos.DrawLine(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position);
        }
    }
}
