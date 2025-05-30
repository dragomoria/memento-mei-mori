using UnityEngine;
using System.Collections;

public class SpawnPickupBehavior : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;
    public Transform SpawnOffset;
    
    private FeatherBehavior currentFeather= null;

    public bool isSpawng {get; set;}

    public bool pickStat {get; set;}
    
    private void RandOffset()
    {
        float ver = Random.Range(-3f, 3f);
        float hor = Random.Range(-4F, 0F);
        float dep = 0f;

        SpawnOffset.transform.position = new Vector3(ver, hor, dep);
    }

    private void SpawnFeather()
    {
        RandOffset();
        currentFeather = FeatherBehavior.Instantiate(FeatherPrefab, SpawnOffset.position, transform.rotation);
    }
    
    void Start()
    {
        isSpawng = false;
        StartCoroutine(SpawnPickup());
    }

    void Update()
    {
        if(currentFeather == null && !isSpawng)
        {
            StartCoroutine(SpawnPickup());
        }
    }

    IEnumerator SpawnPickup()
    {
        isSpawng = true;
        yield return new WaitForSeconds(5);
        SpawnFeather();
        isSpawng = false;
    }

}
