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




    // expose to unity editor 
    [Header("Attack Settings")]
    [Tooltip("Duration of the slash warning before damage is applied.")]
    [SerializeField] private float SlashWarningDuration = .75f;

    [TooltipAttribute("Duration the slash visual stays on screen.")]
    [SerializeField] private float SlashVisualDuration = 1f;

    [Tooltip("Delay time between attacks.")]
    [SerializeField] private float dealayTime = 2.5f;
  
    
    


    private void SkullAttack()
    {
        SkullPrefab = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
    }

    private void Slash()
    {
        float ver = Random.Range(-3f, 3f);
        float hor = Random.Range(-4F, 0F);
        
        
        
        //horizontal slash
        GameObject hLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.identity); // l52 l58 i l70-71 dodaj efekt visualny i dźwiekowy 
        hLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(hLine, SlashVisualDuration);


        //vertical slash
        GameObject vLine = Instantiate(SlashLinePrefab, new Vector3(ver, hor, 0), Quaternion.Euler(0, 0, 90f));
        vLine.transform.localScale = new Vector3(30f, 0.75f, 1f);
        Destroy(vLine, SlashVisualDuration);

        StartCoroutine(ApplayDamage(new Vector3(ver, hor, 0)));

    }

    private void DiagonalSlash()
    {
        Vector3 centre = Spawn.position;

        GameObject dLine1 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, 75f));
        GameObject dLine2 = Instantiate(SlashLinePrefab, centre, quaternion.Euler(0, 0, -75f));

        dLine1.transform.localScale = new Vector3(35f, 0.75f, 1f);
        dLine2.transform.localScale = new Vector3(35f, 0.75f, 1f);
        Destroy(dLine1, SlashVisualDuration);
        Destroy(dLine2, SlashVisualDuration);


        StartCoroutine(ApplayDamage(centre, angle: 45f));
        StartCoroutine(ApplayDamage(centre, angle: -45f));
    }


    IEnumerator ApplayDamage(Vector3 pos, float angle)
    {
        yield return new WaitForSeconds(SlashWarningDuration);

        Vector2 dSize = new Vector2(30f, 0.75f);
        Collider2D[] hitCollider = Physics2D.OverlapBoxAll(pos, dSize, angle);

        foreach (var hit in hitCollider)
            if (hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        
        
    }

    IEnumerator ApplayDamage(Vector3 pos)
    {
        yield return new WaitForSeconds(SlashWarningDuration);


        Collider2D[] hHitCollider = Physics2D.OverlapBoxAll(pos, new Vector2(30f, 0.75f), 0f);
        Collider2D[] vHitCollider = Physics2D.OverlapBoxAll(pos, new Vector2(30f, 0.75f), 90f);


        foreach (var hit in hHitCollider)
            if (hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        foreach (var hit in vHitCollider)
            if(hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();
        
    }

    void Start()
    {
        StartCoroutine(AttackChoice());
    }

    IEnumerator AttackChoice()
    {
        while (true)
        {
            AttackID = 3;
            //Random.Range(0, 4); // add checking if previous attack was already rolled 
            yield return new WaitForSeconds(dealayTime);

            if (currentSkull == null || currentSkull.GetFinished())
            {
                if (AttackID == 0)
                {


                    currentSkull = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
                }
                else if (AttackID == 1)
                {
                    Slash();
                    // DiagonalSlash();
                }
                else if (AttackID == 2)
                {
                    DiagonalSlash();
                    // DiagonalSlash();
                }
                else if (AttackID == 3)
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
