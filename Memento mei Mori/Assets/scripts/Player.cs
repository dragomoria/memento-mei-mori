using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int health;
    public FeatherBehavior FeatherPrefab;
    public SpawnPickupBehavior pawn;
    private bool FeatherStatus;
    private bool curStatus;


    private bool isInvincible = false;

    void Start()
    {
        FeatherStatus = false;
        curStatus = false;
        health = 1;
    }

    void Update()
    {
        curStatus = pawn.isSpawng;

        /*if(curStatus == true)
        {
        FeatherStatus = pawn.pickStat;
        print(FeatherStatus);
        HealthUp();
        }*/
    }

    public void HealthUp()
    {
        health++;
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
