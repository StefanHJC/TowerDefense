using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public event UnityAction<Enemy> Reached;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            gameObject.SetActive(false);
    }
    
    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("Reached");
            Reached?.Invoke(enemy);
        }
    }
}
