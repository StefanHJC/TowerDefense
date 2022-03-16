using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Gate Target { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(Gate target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = true;
    }
}
