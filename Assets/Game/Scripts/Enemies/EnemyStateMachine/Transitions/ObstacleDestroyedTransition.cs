using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyedTransition : Transition
{
    private void Start()
    {
        if (Target.isActiveAndEnabled == false)
            NeedTransit = true;
    }
}
