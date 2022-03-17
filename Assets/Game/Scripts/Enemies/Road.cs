using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _wayPoints;

    public List<WayPoint> Way => _wayPoints;
}
