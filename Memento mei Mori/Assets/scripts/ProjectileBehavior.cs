using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 4.5f;

    private void Update()
    {
        transform.position += -transform.up * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.getDamage();

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
        else destroyAfterTime(2.5f);
    }
    
    IEnumerator destroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

