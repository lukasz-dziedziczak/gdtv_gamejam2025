using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public int CoinsInLevel { get; private set; }

    private void Start()
    {
        CoinsInLevel = FindObjectsByType<CoinPickup>(FindObjectsSortMode.None).Length;

        UI.CoinAmount.UpdateCoinDisplay();
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
        UI.CoinAmount.UpdateCoinDisplay();
        UI.Audio.PlayCoinPickupSound();

        if (Amount >= CoinsInLevel)
        {
            UI.WinScreen.gameObject.SetActive(true);
        }
    }
}
