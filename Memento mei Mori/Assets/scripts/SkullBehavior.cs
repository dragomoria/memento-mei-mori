using UnityEngine;
using System.Collections;

public class SkullBehavior : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    public bool Finished { get; private set; }
    private float totalRotation = 0f;


    void Start()
    {
        Finished = false;
        StartCoroutine(timer());
    }


    IEnumerator timer()
    {
        while (true)
        {
            float rotAmount = 2000f * Time.deltaTime;
            totalRotation += rotAmount;

            transform.localRotation = Quaternion.Euler(0, 0, totalRotation);
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);


            if (totalRotation >= 360f)
            {
                Finished = true;
                Destroy(gameObject);
                yield return new WaitForSeconds(1f);
                yield break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
