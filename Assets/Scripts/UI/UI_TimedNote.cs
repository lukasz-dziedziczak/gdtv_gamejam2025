using UnityEngine;

public class UI_TimedNote : MonoBehaviour
{
    [SerializeField] float lifeTime = 2.5f;
    float timer;

    private void Update()
    {
        if (Player.Instance != null && Player.Instance.Movement.HasMoved)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
