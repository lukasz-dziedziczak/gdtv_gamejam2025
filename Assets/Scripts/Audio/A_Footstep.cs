using UnityEngine;

public class A_Footstep : A_BASE
{
    [SerializeField] AudioClip[] footstepClips;

    public void PlayFootstepSound()
    {
        if (footstepClips.Length == 0) return;
        PlayClip(footstepClips[Random.Range(0, footstepClips.Length)]);
    }
}
