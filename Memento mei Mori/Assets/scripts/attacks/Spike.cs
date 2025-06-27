using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
        yield return spawnWall(Dir.left, attackParams);
    }

    private IEnumerator spawnWall(Dir whereToSpawn, AttackParams attackParams)
    {
        float spikeHeight = spikePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        int spikeCount = (int)(4 / spikeHeight);

        switch (whereToSpawn)
        {
            case Dir.left:
                for (int i = 0; i < spikeCount; i++)
                {
                    Vector3 spawnPos = bottomLeft + new Vector3(0, i * spikeHeight, 0);
                    spikeList.Add(Object.Instantiate(spikePrefab, spawnPos, Quaternion.identity));
                }

                foreach (var spike in spikeList)
                {
                    spike.GetComponent<Rigidbody2D>().linearVelocityX = (float)(attackParams.speed ?? 5f);
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
        yield return new WaitForSeconds(5f);
        foreach (var spike in spikeList)
        {
            Object.Destroy(spike);
        }
        spikeList.Clear();
        GlobalAttackEvent.AttackFinished();
    }

}