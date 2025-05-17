using UnityEngine;

public class A_RifleMuzzle : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float pitchVariation = 0.05f;

    public void PlayFireSound()
    {
        if (audioSource == null || audioClips.Length == 0) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
        audioSource.Play();
    }
}
