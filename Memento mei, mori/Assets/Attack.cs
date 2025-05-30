using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public SkullBehavior SkullPrefab;
    public Transform Spawn;
    private int AttackID;
    private SkullBehavior currentSkull;

    private void SkullAttack()
    {
        SkullPrefab = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
    }

    private void Slash()
    {

    }

    private void DiagonalSlash()
    {

    }

    void Start()
    {
        StartCoroutine(AttackChoice());
    }

    IEnumerator AttackChoice()
    {
        while (true)
        {
            AttackID = Random.Range(0, 3);
            yield return new WaitForSeconds(1);

            if (currentSkull == null || currentSkull.GetFinished())
            {
                if (AttackID == 0)
                {
                    currentSkull = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
                }
                else if (AttackID == 1)
                {
                    Slash();
                }
                else if (AttackID == 2)
                {
                    DiagonalSlash();
                }
            }
        }
    }

}
