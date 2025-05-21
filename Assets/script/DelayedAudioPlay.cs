using System.Collections;
using UnityEngine;

public class DelayedAudioPlay : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip infoClip;       // The audio clip to play
    public float delayTime = 2f;     // Delay time in seconds

    void Start()
    {
        StartCoroutine(PlayAudioWithDelay());
    }

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the delay
        audioSource.clip = infoClip;  // Assign the audio clip
        audioSource.Play();           // Play the audio
    }
}
