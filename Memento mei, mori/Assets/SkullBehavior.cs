using UnityEngine;
using System.Collections;

public class SkullBehavior : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;

    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while(true)
        {
            float rotAmount = 5000 * Time.deltaTime;
            float currentRot = transform.localRotation.eulerAngles.z;

            transform.localRotation = Quaternion.Euler(new Vector3(0,0,currentRot+rotAmount));
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
