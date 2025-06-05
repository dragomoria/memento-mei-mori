using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using System.Numerics;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    public SkullBehavior SkullPrefab;
    public Transform Spawn;
    private int AttackID;
    private SkullBehavior currentSkull;

    public GameObject SlashLinePrefab;
    public GameObject SlashFRPrefab;




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
    


    private void SkullAttack()
    {
        //slashAttack.SetActive(false);

        SkullPrefab = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);

        //slashAttack.SetActive(true);
    }

    private void Slash()
    {
        float ver = Random.Range(-3f, 3f);
        float hor = Random.Range(-4F, 0F);
        
        //magicAttack.SetActive(false);
        //slashAttack.SetActive(true);
        
        //horizontal slash
        GameObject hLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.identity); // l52 l58 i l70-71 dodaj efekt visualny i dzwiekowy 
        hLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(hLine, SlashVisualDuration);


        //vertical slash
        GameObject vLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.Euler(0, 0, 90f));
        vLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(vLine, SlashVisualDuration);

        StartCoroutine(ApplayDamage(new Vector3(ver, hor, 0)));

        //slashAttack.SetActive(false);
        //magicAttack.SetActive(true);
        
    }

    private void DiagonalSlash()
    {
        //magicAttack.SetActive(false);
        //slashAttack.SetActive(true);


        Vector3 centre = Spawn.position;

        GameObject dLine1 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, 75f));
        GameObject dLine2 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, -75f));

        dLine1.transform.localScale = new Vector3(35f, 0.75f, 1f);
        dLine2.transform.localScale = new Vector3(35f, 0.75f, 1f);
        Destroy(dLine1, SlashVisualDuration);
        Destroy(dLine2, SlashVisualDuration);


        StartCoroutine(ApplayDamage(centre, angle: 75f));
        StartCoroutine(ApplayDamage(centre, angle: -75f));

        //slashAttack.SetActive(false);
        //magicAttack.SetActive(true);
    }


    IEnumerator ApplayDamage(Vector3 pos, float angle)
    {
        yield return new WaitForSeconds(SlashWarningDuration);

        GameObject slashFR = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f,0f,angle));
        GameObject slashFR2 = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f,0f,angle));

        yield return new WaitForSeconds(0.5f);

        Destroy(slashFR);
        Destroy(slashFR2);

        Vector2 dSize = new Vector2(30f, 0.75f);
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

    void Start()
    {
        circle.SetActive(false);
        StartCoroutine(AttackChoice());
    }

    IEnumerator AttackChoice()
    {
        while (true)
        {
            AttackID = Random.Range(0, 5);
            // add checking if previous attack was already rolled

            yield return new WaitForSeconds(dealayTime);

            if (currentSkull == null || currentSkull.GetFinished())
            {
                if (AttackID <= 1)
                {
                    circle.SetActive(true);
                    yield return new WaitForSeconds(0.25f);
                    circle.SetActive(false);
                    currentSkull = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
                }
                else if (AttackID <= 3)
                {
                    Slash();
                }
                /*else if (AttackID == 4)
                {
                    DiagonalSlash();
                }*/
                else if (AttackID == 4)
                {
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

}
