using UnityEngine;
using System.Collections;

public class FeatherBehavior : MonoBehaviour
{
    private bool Picked;

    public bool GetPicked()
    {
        return Picked;
    }

    void Start()
    {
        Picked = false;
        StartCoroutine(PickupTimer());
    }

    IEnumerator PickupTimer()
    {
        while(Picked == false)
        {
                yield return new WaitForSeconds(5f);
                Destroy(gameObject);
                yield break;
        }

    }

 
      
      private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Picked = true;
            FeatherCounter.instance.IncreaseFeathers(1);
            Destroy(gameObject);
        }
    }
}
