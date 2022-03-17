using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected CityEnter City { get; private set; }
    protected Enemy Enemy { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(CityEnter city)
    {
        City = city;
    }

    private void OnEnable()
    {
        NeedTransit = false;
        Enemy = GetComponent<Enemy>();
    }
}
