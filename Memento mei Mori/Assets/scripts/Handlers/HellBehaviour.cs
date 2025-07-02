using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class HellBehaviour : MonoBehaviour
{

    
    [SerializeField]private GameObject foreground;
    [SerializeField]private GameObject background;
    [SerializeField]private GameObject mist;


    [SerializeField]private SpriteHandler spriteHandler;

    private Vector3 offset = new Vector3(0, 0.2f, 0);
    private float duration = 2f;


    private void Start()
    {

        StartCoroutine(loopSprite(foreground, duration));
        StartCoroutine(loopSprite(background, duration));
        StartCoroutine(loopSprite(mist, duration));
    }


    private IEnumerator loopSprite(GameObject sprite, float duration)
    {
        Vector3 originalPosition = sprite.transform.position;
        Vector3 up = originalPosition + offset;
        Vector3 down = originalPosition - offset;

        while (true)
        {
            duration = Random.Range(2f, 5f);
            offset = new Vector3(0, Random.Range(0.1f,.4f), 0); // change this to look goooood 
            yield return StartCoroutine(spriteHandler.moveSprite(sprite, down, up, duration));
            yield return StartCoroutine(spriteHandler.moveSprite(sprite, up, down, duration));
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform playerTransform = collision.transform;

            collision.GetComponent<Player>()?.getDamage(playerTransform.position - Vector3.up, 15f);
        }
    }
}
