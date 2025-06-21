
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Skull : IAttackPattern
{
    private GameObject skullPrefab;
    private GameObject projectilePrefab;

    public Skull(GameObject skullPrefab, GameObject projectilePrefab)
    {
        this.skullPrefab = skullPrefab;
        this.projectilePrefab = projectilePrefab;
    }

    public IEnumerator ExecuteAttack(SpriteHandler spriteHandler, AttackParams attackParams)
    {
        Vector3 center = new Vector3(0, -2, 0);
        GameObject skull = Object.Instantiate(skullPrefab, center, quaternion.Euler(0,0,0));
        yield return new WaitForSeconds(attackParams.telegraphDuration ?? 1f);

        for (int i = 0; i < 360f; i += 5)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            GameObject projectile = Object.Instantiate(projectilePrefab, center, rotation);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 direction = rotation * Vector2.right;
            rb.linearVelocity = direction.normalized * (attackParams.speed ?? 5f);

            skull.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, i);


            yield return new WaitForSeconds(0.1f);
        }

        GlobalAttackEvent.AttackFinished();
        Object.Destroy(skull);


    }
}