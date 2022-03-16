using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
    
    private void Start()
    {
        _currentHealth = _health;
    }
}
