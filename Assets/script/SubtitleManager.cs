using UnityEngine;
using TMPro;  // For TextMeshPro
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public TMP_Text subtitleText;  // Text element for subtitles
    public AudioSource audioSource;  // Audio source for voiceover
    public AudioClip[] voiceovers;  // Audio clips for different models
    private string[] subtitles;  // Subtitle text for each model

    private Coroutine subtitleCoroutine;

    void Start()
    {
        // Example subtitles - Add subtitles for each model
        subtitles = new string[]
        {
            "This is an Animal Cell. It contains various organelles such as the nucleus, mitochondria, and ribosomes...",
            "This is a Plant Cell. It has a rigid cell wall, chloroplasts for photosynthesis, and a large vacuole for storage...",
            "This is the Human Heart. It pumps oxygen-rich blood throughout the body using four chambers..."
        };
    }

    public void PlayAudioWithSubtitles(int modelIndex)
    {
        if (audioSource != null && voiceovers[modelIndex] != null)
        {
            audioSource.clip = voiceovers[modelIndex];
            audioSource.Play();

            // Start subtitles
            if (subtitleCoroutine != null)
                StopCoroutine(subtitleCoroutine);
            subtitleCoroutine = StartCoroutine(ShowSubtitles(modelIndex));
        }
    }

    IEnumerator ShowSubtitles(int modelIndex)
    {
        subtitleText.text = "";  // Clear previous subtitles
        string[] words = subtitles[modelIndex].Split(' ');  // Split text into words
        float delay = audioSource.clip.length / words.Length;  // Calculate timing

        foreach (string word in words)
        {
            subtitleText.text += word + " ";  // Add words one by one
            yield return new WaitForSeconds(delay);
        }
    }

    public void StopSubtitles()
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);
        subtitleText.text = "";  // Clear text
    }
}
