using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float time = 3.0f;
    float timer;

    private void OnEnable()
    {
        player.Health.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        player.Health.OnDeath -= OnDeath;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnDeath()
    {
        timer = time;
    }
}
