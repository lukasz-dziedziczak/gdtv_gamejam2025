using UnityEngine;

public class Impact : MonoBehaviour
{
    ParticleSystem impactParticles;

    private void Awake()
    {
        impactParticles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!impactParticles.isPlaying) Destroy(gameObject);
    }
}
