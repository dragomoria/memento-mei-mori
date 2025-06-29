using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.2f;

    [SerializeField]
    private float magnitude = 1f;
    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float t = 0f;
        float x, y;

        while (t < duration)
        {
            x = Random.Range(-1f, 1f) * magnitude;
            y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPosition + new Vector3(x, y);

            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }

    public void startShake()
    {
        StartCoroutine(Shake());
    }
    
}