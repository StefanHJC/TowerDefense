using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _wayPoints;

    public List<WayPoint> Way => _wayPoints;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _wayPoints.Count - 1; i++)    
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position);
        }
    }
}
