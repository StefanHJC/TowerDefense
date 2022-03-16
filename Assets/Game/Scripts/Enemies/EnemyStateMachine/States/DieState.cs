using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    private void Start()
    {
        Animator.Play(EnemyAnimatorController.States.Die);
    }
}
