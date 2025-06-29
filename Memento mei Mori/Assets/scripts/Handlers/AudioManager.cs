using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Pitch Randomisation")]
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    [Header("Audio soruces")]
    public AudioSource ostSource;
    public AudioSource hitSource;

    public void playHitSFX()
    {
        hitSource.pitch = Random.Range(minPitch, maxPitch);
        hitSource.Play();

        return;
    }

}
