using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private Transform _targetPosition;

    private void Start()
    {
        //_transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
        _targetPosition = transform;
        Target.Reached += OnGateReached;
    }

    private void OnDisable()
    {
        Target.Reached -= OnGateReached;
    }

    private void OnGateReached(Enemy enemy)
    {
        if (enemy.gameObject == this.gameObject)
            NeedTransit = true;
    }
}
