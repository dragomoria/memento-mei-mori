using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Spike : IAttackPattern
{
    private enum Dir
    {
        left,
        right
    };

    private Vector3 bottomRight = new Vector3(3, -4, 0);
    private Vector3 bottomLeft = new Vector3(-3, -4, 0); // the right one is just negate the x value and flip in x axis 
    private GameObject spikePrefab;

    private List<GameObject> spikeList = new List<GameObject>();

    public Spike(GameObject spikePrefab)
    {
        this.spikePrefab = spikePrefab;
    }

    public IEnumerator ExecuteAttack(SpriteHandler spriteHandler, AttackParams attackParams)
    {
        Debug.Log("Staart attack spike");
        yield return spawnWall(Dir.left, attackParams, spriteHandler);
    }

    private IEnumerator spawnWall(Dir whereToSpawn, AttackParams attackParams, SpriteHandler spriteHandler)
    {

        float spikeHeight = spikePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        float spikeLength = spikePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        int spikeCount = (int)(4 / spikeHeight);
        switch (whereToSpawn)
        {
            case Dir.left:
                for (int i = 0; i < spikeCount; i++)
                {
                    Vector3 spawnPos = bottomLeft + new Vector3(-spikeLength, i * spikeHeight, 0);
                    spikeList.Add(Object.Instantiate(spikePrefab, spawnPos, Quaternion.identity));
                }

                foreach (var spike in spikeList)
                {
                    Vector3 start = spike.transform.position;
                    Vector3 end = start + new Vector3(spikeLength, 0, 0);
                    spriteHandler.StartCoroutine(spriteHandler.moveSpike(spike ,start, end, .5f)); // delegate the parallel execution to sprite handlere
                }
                break;
            case Dir.right:
                // TODO: spawn wall to the right
                break;
            default:
                Debug.Log("Not correct direction of wall of spikes");
                break;
        }

        Debug.Log("finished spikewall");
        yield return new WaitForSeconds(1f); // wait until spikes are in plce 

        GlobalAttackEvent.AttackFinished(); // flag that you can start different attack

        yield return new WaitForSeconds((float) (attackParams.duration ?? 5f)); // how long does the spikes last for 


        // cleanup 
        foreach (var spike in spikeList)
        {
            Object.Destroy(spike);
        }
        spikeList.Clear();
    }


    

}