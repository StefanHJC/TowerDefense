using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Gate Target { get; set; }

    public void Enter(Gate gate)
    {
        Target = gate;
        enabled = true;

        foreach (var transition in _transitions)
        {
            transition.enabled = true;
            transition.Init(Target);
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNext()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }
}
