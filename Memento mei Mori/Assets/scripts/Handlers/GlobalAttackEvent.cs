using System;
using UnityEngine;

public static class GlobalAttackEvent
{
    public static event Action onAttackFinished;
    public static event Action spikeReady;
    public static event Action skullReady;

    public static void AttackFinished()
    {
        // Debug.Log("Attack finished");
        onAttackFinished?.Invoke();
    }
    public static void SpikeReady()
    {
        spikeReady?.Invoke();
    }
    public static void SkullReady()
    {
        skullReady?.Invoke();
    }
 
}