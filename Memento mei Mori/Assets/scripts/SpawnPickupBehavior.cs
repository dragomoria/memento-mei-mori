using UnityEngine;
using System.Collections;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpawnPickupBehavior : MonoBehaviour
{
    public FeatherBehavior FeatherPrefab;

    
    private FeatherBehavior currentFeather= null;
    
    private Transform playerTransform;


    public bool isSpawng { get; set; }

    public bool pickStat {get; set;}

    
    
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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


    Vector3 getRandomPoint(Vector2 min, Vector2 max, Vector2 playerPos, float exlusionSize = 2f)
    {
        Debug.Log("playerpos: " + playerPos);
        Vector3 randomPoint;
        Rect exclusion = new Rect(playerPos.x - exlusionSize / 2, playerPos.y - exlusionSize / 2, exlusionSize, exlusionSize);

        //float ver = Random.Range(-3f, 3f);
        // float hor = Random.Range(-4F, 0F);
        do
        {
            float x = Random.Range(min.x, max.x);
            float y = Random.Range(min.y, max.y);
            randomPoint = new Vector3(x, y, 0f);
        } while (exclusion.Contains(randomPoint));

        return randomPoint;
    }



    private void SpawnFeather()
    {
        Vector3 pos = getRandomPoint(new Vector2(-3f, -4f), new Vector2(3f, 0f), playerTransform.position);
        currentFeather = FeatherBehavior.Instantiate(FeatherPrefab, pos, transform.rotation);
        // Debug.Log("Spawned feather at: " + pos);
        if (pos.x < -3f || pos.x > 3f || pos.y < -4f || pos.y > 0f)
        {
            Debug.LogWarning("Feather spawned OUSTSIDE of the defined area!!!!!!!!!!!!!!!!" + pos);
        }
        else
        {
            Debug.Log("Feather spawned within the defined area." + pos  );
        }
    }

    IEnumerator SpawnPickup()
    {
        isSpawng = true;
        yield return new WaitForSeconds(5f);
        SpawnFeather();
        isSpawng = false;
    }

}
