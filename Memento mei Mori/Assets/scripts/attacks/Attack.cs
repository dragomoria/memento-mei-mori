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
    
    [SerializeField] public bool attacksOff = true;
    public AttackType attackType;
    private SpriteHandler spriteHandler;


    private bool readyToAttack;
    private bool spikesReady= true;





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




//refactor the attackID usage, make attackID atomic, use switch statement, create coroutines for combos  and i dont fucking know add some vfxs ig
    IEnumerator AttackChoice()
    {
        if (attacksOff) yield return new WaitForSeconds(100f);
        int attackID;
        int prevAttackID=-1;
        while (true)
        {
            attackID = Random.Range(1, 5);
            while (attackID == prevAttackID) attackID = Random.Range(1, 5);
            prevAttackID = attackID;// swap that with function that has skewness and cannot allow repetitions. 
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
            else if (!spikesReady && attackID == 1)
            {
                yield return new WaitForSeconds(0.49f);
                // Debug.Log("spikes not ready to attack yet");
                continue;
            }
            if (attackID == 2)
            {
                attackManager.selectAttack(AttackType.skull, new AttackParams
                {
                    //= new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0),
                    rotation = Random.Range(0f, 360f),
                    frequency = Random.Range(0.02f, .15f),
                    duration = Random.Range(1f, 5f),
                    speed = Random.Range(3f, 10f),
                    telegraphDuration = Random.Range(0.2f, 1.5f),
                    attackDuration = Random.Range(0.3f, 2.5f),
                    spriteDuration = Random.Range(0.5f, 2f),
                    attackType = this.attackType
                });
            }
            else if (attackID == 3)
                for (int i = 0; i < 4; i++)
                {
                    attackManager.selectAttack(AttackType.slash, new AttackParams
                    {
                        position = null,
                        rotation = Random.Range(-20f, 20f),
                        frequency = Random.Range(0.5f, 1.5f),
                        duration = Random.Range(1f, 2f),
                        speed = Random.Range(4f, 6f),
                        telegraphDuration = 1f,
                        attackDuration = 0.2f,
                        spriteDuration = 2.5f,
                        attackType = this.attackType
                    });
                    yield return new WaitForSeconds(0.05f);
                }
            else
            {
                // attackManager.selectAttack(AttackType.skull, new AttackParams());
                for (int i = 0; i < 4; i++)
                {
                    attackManager.selectAttack(AttackType.slash, new AttackParams
                    {
                        position = null,
                        rotation = Random.Range(-180f, 180f),
                        frequency = Random.Range(0.1f, 1.5f),
                        duration = Random.Range(.5f, 2f),
                        speed = Random.Range(2f, 4f),
                        telegraphDuration = 1f,
                        attackDuration = 0.2f,
                        spriteDuration = 2.5f,
                        attackType = this.attackType
                    });
                    yield return new WaitForSeconds(0.25f);
                }
            }

            readyToAttack = false;
            yield return new WaitForSeconds(1.5f);
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
