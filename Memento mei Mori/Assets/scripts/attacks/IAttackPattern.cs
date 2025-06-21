using System;
using System.Collections;
using UnityEngine;

public interface IAttackPattern
{
    IEnumerator ExecuteAttack(SpriteHandler spriteHandler, AttackParams attackParams);
}