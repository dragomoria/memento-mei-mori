using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;
    public SpawnPickupBehavior pawn;
    private bool curStatus;
    private FeatherCounter featherManger;
    
    [SerializeField]
    private Transform selfTransform;
    
    [SerializeField]
    private Rigidbody2D playerRb;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject damageVFXPrefab;


    private float knocbackForce = 5f;

    private bool isInvincible = false;

    void Start()
    {
        curStatus = false;
        featherManger = FeatherCounter.instance;
    }

    void Update()
    {
        curStatus = pawn.isSpawng;
    }

    public void HealthUp()
    {
        featherManger.IncreaseFeathers(1);;
    }

    private void Dead()
    {

        //fade out czerwony
        SceneManager.LoadScene("BadEnding");
    }

    private void WinTheGame()
    {
        SceneManager.LoadScene("GoodEnding");
    }

    //add response to damage by extracting func of getting damage from OnCollisionEnter2D and getDamage method and add there effetcs visual and sound

    public void getDamage()
    {
        GameObject vfx = Instantiate(damageVFXPrefab, selfTransform.position, quaternion.identity);
        Destroy(vfx, 2f);


        if (isInvincible)
            return;
        else
            StartCoroutine(InvincibilityFrames(1f));
        
        int health = featherManger.currentFeathers;
        health--;
        FeatherCounter.instance.DecreaseFeathers(1);
        if (health <= 0)
        {
            Dead();
        }
        if (health >= 20)
        {
            WinTheGame();
        }
    }
    public void getDamage(Vector3 directionFrom, float? knocbackForce)
    {
        Debug.Log($"Body Type: {playerRb.bodyType}");

        Vector2 knocbackDircetion = (selfTransform.position - directionFrom).normalized;
        Debug.Log($"applying force towards {knocbackDircetion}");

        playerMovement.applyKnockback();
        // playerRb.AddForce(knocbackDircetion * knocbackForce, ForceMode2D.Impulse);
        playerRb.linearVelocity = knocbackDircetion * (float) (knocbackForce ?? this.knocbackForce);
        
        if (isInvincible)
            return;
        getDamage();
    }

    IEnumerator InvincibilityFrames(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    //invis frames



}
