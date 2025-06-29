using System.Collections;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform selfTransform;
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.getDamage(selfTransform.position, null);
        
    }
}

