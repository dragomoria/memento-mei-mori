using UnityEngine;
using System.Collections;

public class SkullBehavior : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    private bool Finished;
    private float totalRotation = 0f;

    public bool GetFinished() => Finished;

    void Start()
    {
        Finished = false;
        StartCoroutine(timer());
    }

    /*IEnumerator timer()
    {

        while (true)
        {
            if (this.transform.rotation.eulerAngles.z >= 359)
            {
                
                break;
            }
            float rotAmount = 5000 * Time.deltaTime;
            float currentRot = transform.localRotation.eulerAngles.z;

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRot + rotAmount));
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            yield return new WaitForSeconds(0.15f);
        }
    }*/

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

            yield return new WaitForSeconds(0.15f);
        }
    }
}
