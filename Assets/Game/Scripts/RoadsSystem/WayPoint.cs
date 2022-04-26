using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class WayPoint : MonoBehaviour
{
    private SphereCollider _collider;

    public event UnityAction<Enemy> Reached;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            Reached?.Invoke(enemy);
    }
}
