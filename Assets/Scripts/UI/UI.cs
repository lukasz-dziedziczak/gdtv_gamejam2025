using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] UI_Ammo ammo;
    [SerializeField] UI_HealthBar healthBar;
    [SerializeField] UI_CoinAmount coinAmount;
    [SerializeField] A_UI sound;
    [SerializeField] UI_Crosshair crosshair;

    public static UI_Ammo Ammo => Instance.ammo;
    public static UI_HealthBar HealthBar => Instance.healthBar;
    public static UI_CoinAmount CoinAmount => Instance.coinAmount;
    public static A_UI Audio => Instance.sound;
    public static UI_Crosshair Crosshair => Instance.crosshair;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
