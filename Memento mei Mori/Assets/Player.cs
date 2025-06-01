using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int health;
    public FeatherBehavior FeatherPrefab;
    public SpawnPickupBehavior pawn;
    private bool FeatherStatus;
    private bool curStatus;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Harmful")
        {
            health--;
            FeatherCounter.instance.DecreaseFeathers(1);
        }

        if (health <= 0)
        {
            Dead();
        }

        if (health >= 20)
        {
            WinTheGame();
        }
    }

    //add response to damage by extracting func of getting damage from OnCollisionEnter2D and getDamage method and add there effetcs visual and sound

    public void getDamage()
    {
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
        ;
    }


    //invis frames



}
