using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    FeatherBehavior FeatherPrefab;
    private bool FeatherStatus;

    void Start()
    {
        health = 1;
    }

    private void HealthUp()
    {
        FeatherStatus = FeatherPrefab.GetPicked();

        if(FeatherStatus == true)
        {
            health++;
            print(health);
        }
    }
}
