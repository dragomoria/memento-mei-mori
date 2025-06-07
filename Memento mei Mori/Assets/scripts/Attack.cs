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

public class Attack : MonoBehaviour
{

    public enum AttackType
    {
        slash,
        magic,
    }
    
    public AttackType attackType;

    

    private int AttackID;
    private SkullBehavior currentSkull;

    public GameObject SlashLinePrefab;
    public GameObject SlashFRPrefab;

    private SpriteHandler spriteHandler;




    // expose to unity editor 
    [Header("Attack Settings")]
    [Tooltip("Duration of the slash warning before damage is applied.")]
    [SerializeField] private float SlashWarningDuration = 1f;

    [TooltipAttribute("Duration the slash visual stays on screen.")]
    [SerializeField] private float SlashVisualDuration = 2.5f;

    [Tooltip("Delay time between attacks.")]
    [SerializeField] private float dealayTime = 2.5f;

    [SerializeField] private GameObject magicAttack;
    [SerializeField] private GameObject slashAttack;
    [SerializeField] private GameObject circle;
    [SerializeField] private Transform Spawn;
    [SerializeField] private SkullBehavior skullPrefab;

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
        

    private void SkullAttack()
    {
        attackType = AttackType.magic;
        spriteHandler.showMagicAttack();
        currentSkull = Instantiate(skullPrefab, Spawn.position, transform.rotation);
        if (currentSkull.Finished)
            StartCoroutine(hideGameObject(attackType, 0f));

        
    }

    private void Slash()
    {
        attackType = AttackType.slash;
        spriteHandler.showSlash();
        float ver = Random.Range(-3f, 3f);
        float hor = Random.Range(-4F, 0F);
        
        
        //horizontal slash
        GameObject hLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.identity); // l52 l58 i l70-71 dodaj efekt visualny i dzwiekowy 
        hLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(hLine, SlashVisualDuration);


        //vertical slash
        GameObject vLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.Euler(0, 0, 90f));
        vLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(vLine, SlashVisualDuration);

        StartCoroutine(ApplayDamage(new Vector3(ver, hor, 0)));
        StartCoroutine(hideGameObject(attackType, 5f));

        
    }

    private void DiagonalSlash()
    {
        attackType = AttackType.slash;
        spriteHandler.showSlash();

        Vector3 centre = Spawn.position;

        GameObject dLine1 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, 75f));
        GameObject dLine2 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, -75f));

        dLine1.transform.localScale = new Vector3(30f, 0.75f, 1f);
        dLine2.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(dLine1, SlashVisualDuration);
        Destroy(dLine2, SlashVisualDuration);


        StartCoroutine(ApplayDamage(centre, angle: 75f));
        StartCoroutine(ApplayDamage(centre, angle: -75f)); 
        StartCoroutine(hideGameObject(attackType, 2.5f));
    }


    IEnumerator hideGameObject(AttackType attack,float delay)
    {
        yield return new WaitForSeconds(delay);
        switch (attack)
        {
            case AttackType.slash:
                spriteHandler.hideSlash();
                break;
            case AttackType.magic:
                spriteHandler.hideMagicAttack();
                break;
        }
    }

    IEnumerator ApplayDamage(Vector3 pos, float angle)
    {
        yield return new WaitForSeconds(SlashWarningDuration);

        GameObject slashFR = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f,0f,angle));
        GameObject slashFR2 = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f,0f,angle));

        yield return new WaitForSeconds(0.5f);

        Destroy(slashFR);
        Destroy(slashFR2);

        Vector2 dSize = new Vector2(.5f, 30f);
        Collider2D[] hitCollider = Physics2D.OverlapBoxAll(pos, dSize, angle);

        foreach (var hit in hitCollider)
            if (hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        
        
    }

    IEnumerator ApplayDamage(Vector3 pos)
    {
        yield return new WaitForSeconds(SlashWarningDuration);

        GameObject slashFR = Instantiate(SlashFRPrefab, pos, quaternion.identity);
        GameObject slashFR2 = Instantiate(SlashFRPrefab, pos, quaternion.identity);
        slashFR.transform.rotation *= Quaternion.Euler(0, 0, 90);

        yield return new WaitForSeconds(0.5f);

        Destroy(slashFR);
        Destroy(slashFR2);

        Collider2D[] hHitCollider = Physics2D.OverlapBoxAll(pos, new Vector2(30f, 0.75f), 0f);
        Collider2D[] vHitCollider = Physics2D.OverlapBoxAll(pos, new Vector2(30f, 0.75f), 90f);


        foreach (var hit in hHitCollider)
            if (hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        foreach (var hit in vHitCollider)
            if(hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        
    }


    IEnumerator AttackChoice()
    {
        while (true)
        {
            AttackID = Random.Range(1, 5);
            yield return new WaitForSeconds(dealayTime);
            if (AttackID <= 1){

                spriteHandler.showSkullAttack();
                yield return new WaitForSeconds(0.25f);
                SkullAttack();
            }
            else if (AttackID <= 3){

                Slash();
            }
            else if (AttackID == 9){

                DiagonalSlash();
            }
            else if (AttackID == 4){

                for (int i = 0; i < 4; i++)
                {
                    Slash();
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(1f);
            }
            
        }
    }

}
