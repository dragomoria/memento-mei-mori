using UnityEngine;

public class HellBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform playerTransform = collision.transform;

            collision.GetComponent<Player>()?.getDamage(playerTransform.position - Vector3.up, 15f);
        }
    }
}
