using UnityEngine;

public class A_Footstep : A_BASE
{
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] float pitchVariation = 0.05f;

    public void PlayFootstepSound()
    {
        if (footstepClips.Length == 0) return;
        audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
        PlayClip(footstepClips[Random.Range(0, footstepClips.Length)]);
        print("footstep");
    }
}
