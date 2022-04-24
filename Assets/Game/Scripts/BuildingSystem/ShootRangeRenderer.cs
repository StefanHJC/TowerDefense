using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShootRangeRenderer : MonoBehaviour
{
    [Range(0, 50)]
    [SerializeField] private int _segments;
    [Range(0, 5)]
    [SerializeField] private float _xRadius;
    [Range(0, 5)]
    [SerializeField] private float _zRadius;
    
    private LineRenderer _line;

    public void Init(float radius)
    {
        _xRadius = radius;
        _zRadius = radius;
    }

    public void Disable()
    {
        Destroy(this);
        Destroy(_line);
    }

    private void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _segments = 50;

        _line.SetVertexCount(_segments + 1);
        _line.useWorldSpace = false;
        CreatePoints();
    }

    private void CreatePoints()
    {
        float x;
        float z;

        float angle = 20f;

        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _xRadius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * _zRadius;

            _line.SetPosition(i, new Vector3(x, 0.3f, z));
            _line.material.color = Color.red;
            angle += (360f / _segments);
        }
    }
}