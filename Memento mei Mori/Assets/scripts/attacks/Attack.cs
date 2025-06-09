using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using System.Numerics;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public enum AttackType
    {
        slash,
        diagonalSlash,
        skull,
    }

public class Attack : MonoBehaviour
{
    public AttackType attackType;
    private SpriteHandler spriteHandler;




    // expose to unity editor 
    [Header("Attack Settings")]
    [Tooltip("Duration of the slash warning before damage is applied.")]


    [SerializeField] private float dealayTime = 2.5f;
    [Tooltip("Delay time between attacks.")]
    [SerializeField] private AttackHandler attackManager;

    private void Start()
    {
        spriteHandler = SpriteHandler.instance;
        if (spriteHandler == null)
        {
            Debug.LogError("SpriteHandler instance not found!");
            return;
        }
        StartCoroutine(AttackChoice());
    }

    private AttackType mapIDToAttackType(int id)
    {
        switch (id)
        {
            case 0:
                return AttackType.slash;
            case 1:
                return AttackType.diagonalSlash;
            case 2:
                return AttackType.skull;
            default:
                Debug.LogWarning("Unknown attack ID: " + id);
                return AttackType.slash; // Default to slash if unknown
        }
    }

    IEnumerator AttackChoice()
    {
        int AttackID;
        while (true)
        {
            //add a flag to check if the attack finished, dont allow for room for breath
            // AttackID = Random.Range(0, 3); // randomise it 
            // attackType = mapIDToAttackType(AttackID);
            for (int i = 0; i < 7; i++)
            {
                attackManager.selectAttack(AttackType.slash, new AttackParams
                {
                    position = null,
                    rotation = Random.Range(-20f, 20f),
                    frequency = Random.Range(0.5f, 1.5f),
                    duration = Random.Range(1f, 2f),
                    speed = Random.Range(1f, 3f),
                    telegraphDuration = 1f,
                    attackDuration = 0.2f,
                    spriteDuration = 2.5f,
                    attackType = this.attackType
                });
                yield return new WaitForSeconds(Random.Range(0.05f, 0.3f)); // can randomise it based on previous attack, or dependin on feather count
            }

            yield return new WaitForSeconds(5f); // can randomise it based on previous attack, or dependin on feather count
        }
    }

}
