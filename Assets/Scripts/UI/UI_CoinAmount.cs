using TMPro;
using UnityEngine;

public class UI_CoinAmount : MonoBehaviour
{
    [SerializeField] TMP_Text coincount;

    public void UpdateCoinDisplay()
    {
        coincount.text = Player.Instance.CoinHolder.Amount.ToString();
    }
}
