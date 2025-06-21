using System;
using UnityEngine;

public static class GlobalAttackEvent
{
    public static event Action onAttackFinished;

    public static void AttackFinished()
    {
        onAttackFinished?.Invoke();
    }
 
}