using UnityEngine;

public class A_BASE : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    protected void PlayClip(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
