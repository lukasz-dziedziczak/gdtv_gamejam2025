using TMPro;
using UnityEngine;

public class UI_Ammo : MonoBehaviour
{
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text storateAmmoText;

    public void UpdateAmmoText()
    {
        currentAmmoText.text = Player.Instance.Shooting.CurrentAmmo.ToString();
        storateAmmoText.text = Player.Instance.Shooting.StorageAmmo.ToString();
    }
}
