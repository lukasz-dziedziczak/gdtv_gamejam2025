using TMPro;
using UnityEngine;

public class UI_CoinAmount : MonoBehaviour
{
    [SerializeField] TMP_Text coincount;

    CoinHolder coins => Player.Instance.CoinHolder;

    private void Start()
    {
        UpdateCoinDisplay();
    }

    public void UpdateCoinDisplay()
    {
        if (Player.Instance == null) return;
        coincount.text = coins.Amount.ToString() + " / " + coins.CoinsInLevel.ToString();
    }
}
