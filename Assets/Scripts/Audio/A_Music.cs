using UnityEngine;

public class A_Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] musicClips;
    int index = -1;

    private void Update()
    {
        if (audioSource == null || musicClips.Length == 0) return;

        if (!audioSource.isPlaying)
        {
            index++;
            if (index >= musicClips.Length) index = 0;

            audioSource.clip = musicClips[index];
            audioSource.Play();
        }
    }
}
