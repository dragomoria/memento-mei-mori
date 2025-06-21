using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackHandler : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] SpriteHandler spriteHandler;

    [SerializeField] private GameObject slashHitboxPrefab;
    [SerializeField] private GameObject slashLinePrefab;
    [SerializeField] private GameObject skullPrefab;
    [SerializeField] private GameObject projectilePrefab;

    AttackType attackType;
    private Dictionary<AttackType, IAttackPattern> patternMap;

    void Awake()
    {
        patternMap = new Dictionary<AttackType, IAttackPattern>
        {
            { AttackType.slash, new Slash(slashLinePrefab, slashHitboxPrefab) },
            { AttackType.skull, new Skull(skullPrefab, projectilePrefab)}
        };
    }


    public void selectAttack(AttackType attack, AttackParams attackParams)
    {
        // Debug.Log($"Selected attack: {attack} with params: {attackParams}");
        if (patternMap.TryGetValue(attack, out var pattern))
            StartCoroutine(pattern.ExecuteAttack(spriteHandler, attackParams));
        else
            Debug.LogWarning($"Attack type {attack} not found in pattern map.");
    }



    // IEnumerator hideGameObject(AttackType attack, float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     switch (attack)
    //     {
    //         case AttackType.slash:
    //             spriteHandler.hideSlash();
    //             break;
                
    //         case AttackType.skull:
    //             spriteHandler.hideMagicAttack();
    //             break;
    //     }
    // }

}
