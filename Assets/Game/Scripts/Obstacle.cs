using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] protected int Health;

    protected int CurrentHealth;

    public event UnityAction Destroyed;

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            Destroyed?.Invoke();
        }
    }
    
    private void Start()
    {
        CurrentHealth = Health;
    }
}
