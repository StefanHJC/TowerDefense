using UnityEngine;
using UnityEngine.Events;

public class WayPoint : MonoBehaviour
{
    public event UnityAction Reached;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            Reached?.Invoke();
    }
}
