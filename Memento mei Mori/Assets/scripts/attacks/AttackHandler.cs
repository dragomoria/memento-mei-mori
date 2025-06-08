using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackHandler : MonoBehaviour
{

    AttackType attackType;

    [Header("Attack Settings")]
    [SerializeField] SpriteHandler spriteHandler;

    [SerializeField] private GameObject SlashFRPrefab;
    [SerializeField] private GameObject SlashLinePrefab;

    [SerializeField] private float SlashVisualDuration = 2.5f;
    [SerializeField] private float SlashWarningDuration = 1f;
    [SerializeField] private Transform Spawn;


    public void selectAttack(AttackType attackType)
    {
        this.attackType = attackType; // set class varibale

        switch (attackType)
        {
            case AttackType.slash:
                // Slash();
                break;
            case AttackType.diagonalSlash:
                // DiagonalSlash();
                break;
            case AttackType.skull:
            Debug.LogWarning("Skull attack is not implemented yet.");
                break;
            default:
                Debug.LogWarning("Unknown attack type selected: " + attackType);
                break;
        }
    }


    IEnumerator ApplayDamage(Vector3 pos, float angle)
    {
        yield return new WaitForSeconds(SlashWarningDuration);

        GameObject slashFR = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f, 0f, angle));
        GameObject slashFR2 = Instantiate(SlashFRPrefab, pos, quaternion.Euler(0f, 0f, angle));

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
            if (hit.CompareTag("Player")) hit.GetComponent<Player>().getDamage();

    }

    IEnumerator hideGameObject(AttackType attack, float delay)
    {
        yield return new WaitForSeconds(delay);
        switch (attack)
        {
            case AttackType.slash:
                spriteHandler.hideSlash();
                break;
                
            case AttackType.skull:
                spriteHandler.hideMagicAttack();
                break;
        }
    }

}
