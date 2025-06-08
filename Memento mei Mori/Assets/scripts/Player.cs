using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;
    public SpawnPickupBehavior pawn;
    private bool curStatus;
    private FeatherCounter featherManger;

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

    IEnumerator InvincibilityFrames(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    //invis frames



}
