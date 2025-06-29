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
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public enum AttackType
{
    slash,
    diagonalSlash,
    skull,
    spikes,
    }

public class Attack : MonoBehaviour
{
    public AttackType attackType;
    private SpriteHandler spriteHandler;


    private bool readyToAttack;
    private bool spikesReady= true;



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


    IEnumerator AttackChoice()
    {
        int attackID = 1;
        while (true)
        {
            attackID = Random.Range(1, 3); // swap that with function that has skewness and cannot allow repetitions. 
            // Debug.Log(attackID);
            if (!readyToAttack)
            {
                // Debug.Log("not ready to attack");
                yield return new WaitForSeconds(0.49f);
                continue;
            }
            if (attackID == 1 && spikesReady)
            {
                attackManager.selectAttack(AttackType.spikes, new AttackParams());
                spikesReady = false;
            }
            else if (!spikesReady && attackID ==1)
            {
                yield return new WaitForSeconds(0.49f);
                // Debug.Log("spikes not ready to attack yet");
                continue;
            }
            if (attackID == 2 )
            {
                attackManager.selectAttack(AttackType.skull, new AttackParams());
            }
            // attackManager.selectAttack(AttackType.slash, new AttackParams
            // {
            //     position = null,
            //     rotation = Random.Range(-20f, 20f),
            //     frequency = Random.Range(0.5f, 1.5f),
            //     duration = Random.Range(1f, 2f),
            //     speed = Random.Range(1f, 3f),
            //     telegraphDuration = 1f,
            //     attackDuration = 0.2f,
            //     spriteDuration = 2.5f,
            //     attackType = this.attackType
            // });

            readyToAttack = false;
            yield return new WaitForSeconds(1f);
        }
    }


    private void onAttackFinished()
    {
        readyToAttack = true;
    }
    private void onSpikeReady()
    {
        spikesReady = true;
    }

    void OnEnable()
    {
        GlobalAttackEvent.onAttackFinished += onAttackFinished;
        GlobalAttackEvent.spikeReady += onSpikeReady;
    }
    void OnDisable()
    {
        GlobalAttackEvent.spikeReady -= onSpikeReady;
        GlobalAttackEvent.onAttackFinished -= onAttackFinished;
    }

}
