using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;

    private Transform _target;
    private Vector3 _targetPosition;

    public void SetTarget(Transform target)
    {
        _target = target;
        _targetPosition = target.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.isActiveAndEnabled)
                enemy.TakeDamage(_damage);

            Destroy(gameObject);        
        }
    }
}
