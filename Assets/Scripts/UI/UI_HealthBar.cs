using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] Image bar;
    bool initilized;

    private void OnEnable()
    {
        Initilize();
    }

    private void OnDisable()
    {
        Player.Instance.Health.OnDamaged -= OnDamaged;
        Player.Instance.Health.OnDeath -= UpdateHealthBar;
    }

    public void Initilize()
    {
        if (initilized) return;

        if (Player.Instance == null || Player.Instance.Health == null) return;

        Player.Instance.Health.OnDamaged += OnDamaged;
        Player.Instance.Health.OnDeath += UpdateHealthBar;
        initilized = true;

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        bar.fillAmount = Player.Instance.Health.Percentage;
    }

    private void OnDamaged(GameObject damgeDealer)
    {
        UpdateHealthBar();
    }
}
