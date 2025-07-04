using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;
    public SpawnPickupBehavior pawn;
    private bool curStatus;
    
    [SerializeField]
    private Transform selfTransform;
    
    [SerializeField]
    private Rigidbody2D playerRb;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private GameObject damageVFXPrefab;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField] SpriteHandler spriteHandler;


    private float knocbackForce = 5f;

    private bool isInvincible = false;
    private int featherCount = 0;

    void Start()
    {
        curStatus = false;
        setBaseFeathers(10);
    }

    void Update()
    {
        curStatus = pawn.isSpawng;
    }

    private void setBaseFeathers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            collectFeather();
        }
    }
    
    private void Dead(){ SceneManager.LoadScene("BadEnding"); }

    private void WinTheGame(){ SceneManager.LoadScene("GoodEnding"); }


    public void collectFeather()
    {
        spriteHandler.updateFeatherCounter(featherCount);
        featherCount++;
        if (featherCount > 19)
        {
            WinTheGame();
            return;
        }
    }

    public void getDamage()
    {
        GameObject vfx = Instantiate(damageVFXPrefab, selfTransform.position, quaternion.identity);
        Destroy(vfx, 2f);

        audioManager.playHitSFX();

        if (isInvincible)
            return;
        else
            StartCoroutine(InvincibilityFrames(1f));

        cameraShake.startShake();

        if (featherCount <= 0)
        {
            Dead();
            return;
        }
        

        spriteHandler.removeLastFeather(featherCount);
        featherCount--;
    }
    
    public void getDamage(Vector3 directionFrom, float? knocbackForce)
    {
        Debug.Log($"Body Type: {playerRb.bodyType}");

        Vector2 knocbackDircetion = (selfTransform.position - directionFrom).normalized;
        Debug.Log($"applying force towards {knocbackDircetion}");

        playerMovement.applyKnockback();
        playerRb.linearVelocity = knocbackDircetion * (float)(knocbackForce ?? this.knocbackForce);

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





}
