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
        print("skull attack");
        SkullPrefab = SkullBehavior.Instantiate(SkullPrefab, Spawn.position, transform.rotation);
    }

    private void Slash()
    {
        print("slash");
    }

    private void DiagonalSlash()
    {
        print("diagonal slash");
    }

    void Start()
    {
        StartCoroutine(AttackChoice());
    }

    /*IEnumerator AttackChoice()
    {
        while(true)
        {
            AttackID = Random.Range(0,3);

            yield return new WaitForSeconds(1);

            if (AttackID == 0)
            {
                SkullAttack();
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
    }*/

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
                    print("skull attack");
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
