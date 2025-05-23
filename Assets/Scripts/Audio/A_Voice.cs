using UnityEngine;

public class A_Voice : A_BASE
{
    [SerializeField] Player player;
    [SerializeField] AudioClip[] introClips;
    [SerializeField] AudioClip[] killClips;
    [SerializeField] AudioClip[] tookDamageClips;
    [SerializeField] AudioClip[] combatStartClips;
    [SerializeField] AudioClip reloading;
    [SerializeField] AudioClip missionComplete;

    private void OnEnable()
    {
        player.Health.OnDamaged += TookDamage;
        Enemy.Death += PlayKillClip;
    }

    private void OnDisable()
    {
        player.Health.OnDamaged -= TookDamage;
        Enemy.Death -= PlayKillClip;
    }

    private void Start()
    {
        PlayIntroClip();
    }

    public void PlayIntroClip()
    {
        if (introClips.Length > 0)
        {
            PlayClip(introClips[Random.Range(0, introClips.Length)]);
        }
    }

    public void PlayKillClip()
    {
        float random = Random.Range(0.0f, 1.0f);

        if (random < (1.0f/4.0f) && killClips.Length > 0 && !audioSource.isPlaying)
        {
            PlayClip(killClips[Random.Range(0, killClips.Length)]);
        }
    }

    public void PlayTookDamageClip()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < (1.0f / 3.0f) && tookDamageClips.Length > 0 && !audioSource.isPlaying)
        {
            PlayClip(tookDamageClips[Random.Range(0, tookDamageClips.Length)]);
        }
    }

    public void PlayCombatStartClip()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < (1.0f / 6.0f) && combatStartClips.Length > 0 && !audioSource.isPlaying)
        {
            PlayClip(combatStartClips[Random.Range(0, combatStartClips.Length)]);
        }
    }

    private void TookDamage(GameObject damageGiver)
    {
        if (!audioSource.isPlaying)
        {
            PlayTookDamageClip();
        }
        
    }

    public void PlayReloadingClip()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < (1.0f / 2.0f) && !audioSource.isPlaying)
        {
            PlayClip(reloading);
        }
        
    }

    public void PlayMissionCompelte()
    {
        PlayClip(missionComplete);
    }
}
