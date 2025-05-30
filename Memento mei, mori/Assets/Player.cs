using UnityEngine;

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
}
