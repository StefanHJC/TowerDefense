using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private void Start()
    {
        Animator.SetBool(EnemyAnimatorController.States.Run, true);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Enemy.Speed * Time.deltaTime);
    }
}
