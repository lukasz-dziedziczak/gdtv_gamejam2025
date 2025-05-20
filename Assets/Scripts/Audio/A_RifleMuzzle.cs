using UnityEngine;

public class A_RifleMuzzle : A_BASE
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float pitchVariation = 0.05f;
    [SerializeField] AudioClip removeClip;
    [SerializeField] AudioClip loadClip;
    [SerializeField] AudioClip reloadCompleteClip;

    public void PlayFireSound()
    {
        if (audioClips.Length == 0) return;
        audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
        PlayClip(audioClips[Random.Range(0, audioClips.Length)]);
    }

    public void PlayRemoveClipSound()
    {
        PlayClip(removeClip);
    }

    public void PlayLoadClipSound()
    {
        PlayClip(loadClip);
    }

    public void PlayReloadCompleteSound()
    {
        PlayClip(reloadCompleteClip);
    }
}
