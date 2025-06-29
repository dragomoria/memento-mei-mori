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

    private List<GameObject> leftSpikeList = new List<GameObject>();
    
    private List<GameObject> rightSpikeList = new List<GameObject>();

    public Spike(GameObject spikePrefab)
    {
        this.spikePrefab = spikePrefab;
    }

    public IEnumerator ExecuteAttack(SpriteHandler spriteHandler, AttackParams attackParams)
    {
        yield return spawnWall(attackParams, spriteHandler);
    }

    private IEnumerator spawnWall(AttackParams attackParams, SpriteHandler spriteHandler)
    {

        float spikeHeight = spikePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        float spikeLength = spikePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        int spikeCount = (int)(4.8 / spikeHeight);
        for (int i = 0; i < spikeCount; i++)
        {
            Vector3 spawnPos = bottomLeft + new Vector3(-spikeLength, i * spikeHeight, 0);
            leftSpikeList.Add(Object.Instantiate(spikePrefab, spawnPos, Quaternion.identity));
        }

        foreach (var spike in leftSpikeList)

        {
            Vector3 start = spike.transform.position;
            Vector3 end = start + new Vector3(spikeLength, 0, 0);
            spriteHandler.StartCoroutine(spriteHandler.moveSpike(spike, start, end, .5f)); // delegate the parallel execution to sprite handlere
        }

        for (int i = 0; i < spikeCount; i++) // this code is perfect, dont let anybody tell you otherwise ~ Marcel
        {
            Vector3 spawnPos = bottomRight + new Vector3(spikeLength, i * spikeHeight, 0);
            rightSpikeList.Add(Object.Instantiate(spikePrefab, spawnPos, Quaternion.Euler(0, 180, 0)));
        }

        foreach (var spike in rightSpikeList)
        {
            Vector3 start = spike.transform.position;
            Vector3 end = start - new Vector3(spikeLength, 0, 0);
            spriteHandler.StartCoroutine(spriteHandler.moveSpike(spike, start, end, .5f)); // delegate the parallel execution to sprite handlere
        }

        yield return new WaitForSeconds(attackParams.telegraphDuration ?? 0.1f); // wait until spikes are in plce 

        GlobalAttackEvent.AttackFinished(); // flag that you can start different attack

        yield return new WaitForSeconds((float)(attackParams.duration ?? 5f)); // how long does the spikes last for 
        GlobalAttackEvent.SpikeReady();

        // cleanup 
        foreach (var spike in leftSpikeList)
        {
            Object.Destroy(spike);
        }
        leftSpikeList.Clear();
        foreach (var spike in rightSpikeList)
        {
            Object.Destroy(spike);
        }
        rightSpikeList.Clear();
    }


    

}