using UnityEngine;
using System.Collections;

public class FeatherBehavior : MonoBehaviour
{

    public static int featherIndex=0;
    private int localIndex { get; set; }
    private bool Picked;

    public bool GetPicked()
    {
        return Picked;
    }

    void Start()
    {
        Picked = false;
        localIndex = featherIndex;
        featherIndex++;
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


    public void decreaseIndex()
    {
        featherIndex--; 
    }
    
      private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Picked = true;
            collision.GetComponent<Player>()?.collectFeather();
            Destroy(gameObject);
        }
    }
}
