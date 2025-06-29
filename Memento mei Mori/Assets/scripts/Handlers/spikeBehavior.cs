using System.Collections;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform selfTransform;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<Player>()?.getDamage(selfTransform.position);
            }
    }
}

