using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAnimatorController
{
    public static class Params
    {
        public const string Speed = nameof(Speed);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Run = nameof(Run);
        public const string Walk = nameof(Walk);
        public const string Attack = nameof(Attack);
        public const string Die = nameof(Die);
    }
}
