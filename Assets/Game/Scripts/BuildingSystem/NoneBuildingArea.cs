using System.Collections.Generic;
using UnityEngine;

/*PNPOLY - Point Inclusion in Polygon Test W. Randolph Franklin
 * 
 * I run a semi-infinite ray horizontally (increasing x, fixed y) out from the test point, and count how many edges it crosses. 
 * At each crossing, the ray switches between inside and outside. This is called the Jordan curve theorem.
 * The case of the ray going thru a vertex is handled correctly via a careful selection of inequalities. 
 * Don't mess with this code unless you're familiar with the idea of Simulation of Simplicity. 
 * This pretends to shift the ray infinitesimally down so that it either clearly intersects, or clearly doesn't touch. 
 * Since this is merely a conceptual, infinitesimal, shift, it never creates an intersection that didn't exist before, and never destroys an intersection that clearly existed before.
 * 
 * The ray is tested against each edge thus:
 * 1. Is the point in the half-plane to the left of the extended edge? and
 * 2. Is the point's Y coordinate within the edge's Y-range? 
 */
public class NoneBuildingArea : MonoBehaviour
{
    [SerializeField] private List<NoneBuildingAreaPoint> _points;

    public bool IsPointInside(Transform point)
    {
        bool result = false;
        int verticesAmount = _points.Count - 1;
        int i = 0;
        int j = 0;

        for (i = 0, j = verticesAmount - 1; i < verticesAmount; j = i++)
            if ( (_points[i].transform.position.z > point.position.z) != (_points[j].transform.position.z > point.position.z) &&
                (point.position.x < (_points[j].transform.position.x - _points[i].transform.position.x) * (point.position.z - _points[i].transform.position.z) /
                (_points[j].transform.position.z - _points[i].transform.position.z) + _points[i].transform.position.x) )
                    result = !result;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < _points.Count - 1; i++)
        {
            Gizmos.DrawLine(_points[i].transform.position, _points[i + 1].transform.position);
        }
    }
}
