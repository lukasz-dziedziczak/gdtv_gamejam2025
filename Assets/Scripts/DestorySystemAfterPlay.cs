using UnityEngine;

public class DestorySystemAfterPlay : MonoBehaviour
{
    ParticleSystem pSystem;

    private void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (pSystem != null && !pSystem.isPlaying) Destroy(gameObject);
    }
}
