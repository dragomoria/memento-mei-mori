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
            print(health);
    }

    private void Dead()
    {
            SceneManager.LoadScene("Lose");
    }

    private void WinTheGame()
    {
        SceneManager.LoadScene("Win");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Harmful")
        {
            health--;
            FeatherCounter.instance.DecreaseFeathers(1);
        }

        if(health<=0)
        {
            Dead();
        }
        
        if(health>=20)
        {
            WinTheGame();
        }
    }
}
