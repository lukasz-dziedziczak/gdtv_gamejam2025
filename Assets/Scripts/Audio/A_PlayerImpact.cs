using UnityEngine;

public class A_PlayerImpact : A_BASE
{
    [SerializeField] AudioClip[] impactClips;

    public void PlayImpactSound()
    {
        if (impactClips.Length == 0) return;
        PlayClip(impactClips[Random.Range(0, impactClips.Length)]);
    }
}
