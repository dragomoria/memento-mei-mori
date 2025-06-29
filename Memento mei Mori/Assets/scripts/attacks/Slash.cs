using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Slash : IAttackPattern
{

    private GameObject slashLinePrefab;
    private GameObject slashHitboxPrefab;

    public Slash(GameObject slashLinePrefab, GameObject slashHitboxPrefab)
    {
        this.slashLinePrefab = slashLinePrefab;
        this.slashHitboxPrefab = slashHitboxPrefab;
    }

    public IEnumerator ExecuteAttack(SpriteHandler spriteHandler, AttackParams attackParams)
    {
        Debug.Log($"Executing Slash attack with params: {attackParams}");
        spriteHandler.showSlash();
        float ver = attackParams.position?.y ?? Random.Range(-3f, 3f);
        float hor = attackParams.position?.x ?? Random.Range(-4f, 0f);

        Vector3 spawnPosition = new Vector3(ver, hor, 0);
        Quaternion rotation = Quaternion.Euler(0, 0, attackParams.rotation ?? 0f);

        GameObject line = Object.Instantiate(slashLinePrefab, spawnPosition, rotation);

        yield return new WaitForSeconds(attackParams.telegraphDuration ?? 1f);
        Object.Destroy(line); //destroy the visual line after telegraph duration

        yield return ApplyDamage(spriteHandler, attackParams, spawnPosition, rotation);
    }


    public IEnumerator ApplyDamage(SpriteHandler spriteHandler, AttackParams attackParams, Vector3 spawnPosition, Quaternion rotation)
    {
        //spawn the attack line after the telegraph duration
        GameObject hitboxPrefab = Object.Instantiate(slashHitboxPrefab, spawnPosition, rotation);
        BoxCollider2D boxCollider = hitboxPrefab.GetComponent<BoxCollider2D>();
        Vector2 size = Vector2.Scale(boxCollider.size, hitboxPrefab.transform.lossyScale);
        float angle = rotation.eulerAngles.z;

        GlobalAttackEvent.AttackFinished();

        Collider2D[] hits = Physics2D.OverlapBoxAll(spawnPosition, size, angle);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
                hit.GetComponent<Player>().getDamage();
        }

        yield return new WaitForSeconds(attackParams.attackDuration ?? 0.5f);
        Object.Destroy(hitboxPrefab);
        spriteHandler.hideSlash();
    }
}