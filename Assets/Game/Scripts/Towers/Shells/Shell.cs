using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private ShellStats _stats;

    private Enemy _target;

    public void SetTarget(Enemy target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _stats.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.isActiveAndEnabled)
                enemy.TakeDamage(_stats.Damage);

            Explose();
        }
    }

    private void Explose()
    {
        var explosion = Instantiate(_stats.DestroyEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        Destroy(explosion.gameObject, 2f);
        Destroy(gameObject, 2f);
        gameObject.SetActive(false);
    }
}
