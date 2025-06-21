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
using UnityEngine.Rendering;

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


    private bool readyToAttack;



    // expose to unity editor 
    [Header("Attack Settings")]
    [Tooltip("Duration of the slash warning before damage is applied.")]


    [SerializeField] private float dealayTime = 2.5f;
    [Tooltip("Delay time between attacks.")]
    [SerializeField] private AttackHandler attackManager;


    private void Awake()
    {
        readyToAttack = true;
    }

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
        while (true)
        {
            if (!readyToAttack)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            attackManager.selectAttack(AttackType.skull, new AttackParams());
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

            readyToAttack = false;
            yield return new WaitForSeconds(5f);
        }
    }


    private void onAttackFinished()
    {
        readyToAttack = true;
        Debug.Log("successfully run the shit");
    }

    void OnEnable()
    {
        GlobalAttackEvent.onAttackFinished += onAttackFinished;

    }
    void OnDisable()
    {
        
        GlobalAttackEvent.onAttackFinished -= onAttackFinished;
    }

}
