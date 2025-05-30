using UnityEngine;
using System.Collections;

public class SpawnPickupBehavior : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;
    public Transform SpawnOffset;
    
    private FeatherBehavior currentFeather= null;
    
    private void RandOffset()
    {
        float ver = Random.Range(-0.4f, 0.4f);
        float hor = Random.Range(-0.45f, 0.3f);
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
        StartCoroutine(SpawnPickup());
    }

    void Update()
    {
        if(currentFeather == null)
        {
            StartCoroutine(SpawnPickup());
        }
    }

    IEnumerator SpawnPickup()
    {
        yield return new WaitForSeconds(5);
        SpawnFeather();
        yield break;
    }

}
