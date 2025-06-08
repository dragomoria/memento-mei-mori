using UnityEngine;

public class AttackParams
{
    public Vector3? position;
    public float? rotation;
    public float? frequency;
    public float? duration;
    public float? speed;
    public float? telegraphDuration; // how long the attack is visible before it hits
    public float? attackDuration; // how long the warning is visible before the attack hits

    public float spriteDuration;
    public AttackType attackType;

}