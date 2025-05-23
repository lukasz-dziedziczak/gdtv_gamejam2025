using UnityEngine;

public class A_EnemyVoice : A_BASE
{
    [SerializeField] Enemy enemy;
    [SerializeField] AudioClip[] attackClips;
    [SerializeField] AudioClip[] getHitClips;
    [SerializeField] AudioClip[] deathClips;
    [SerializeField] AudioClip[] randomClips;
    [SerializeField] float randomClipTime = 20.0f;
    [SerializeField] float randomClipTimeAdditional = 20.0f;
    [SerializeField] float pitchVariation = 0.10f;

    float timer;

    private void Start()
    {
        ResetTimer();
    }

    private void ResetTimer() { timer = Random.Range(randomClipTime, randomClipTime + randomClipTimeAdditional); }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ResetTimer();
            PlayRandomClip();
        }
    }

    public void PlayAttackClip()
    {
        if (attackClips.Length > 0)
        {
            audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
            PlayClip(attackClips[Random.Range(0, attackClips.Length)]);
        }
    }

    public void PlayGetHitClip()
    {
        if (getHitClips.Length > 0)
        {
            audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
            PlayClip(getHitClips[Random.Range(0, getHitClips.Length)]);
        }
    }

    public void PlayDeathClip()
    {
        if (deathClips.Length > 0)
        {
            audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
            PlayClip(deathClips[Random.Range(0, deathClips.Length)]);
        }
    }

    public void PlayRandomClip()
    {
        if (audioSource.isPlaying || !enemy.IsAlive) return;

        if (randomClips.Length > 0)
        {
            audioSource.pitch = Random.Range(1.0f - pitchVariation, 1.0f + pitchVariation);
            PlayClip(randomClips[Random.Range(0, randomClips.Length)]);
        }
    }
}
