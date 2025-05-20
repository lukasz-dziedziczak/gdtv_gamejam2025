using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    [field: SerializeField] public int Amount { get; private set; }

    private void Start()
    {
        UI.CoinAmount.UpdateCoinDisplay();
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
        UI.CoinAmount.UpdateCoinDisplay();
        UI.Audio.PlayCoinPickupSound();
    }
}
