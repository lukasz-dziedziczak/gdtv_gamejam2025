using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] UI_Ammo ammo;
    [SerializeField] UI_HealthBar healthBar;
    [SerializeField] UI_CoinAmount coinAmount;
    [SerializeField] A_UI sound;
    [SerializeField] UI_Crosshair crosshair;
    [SerializeField] UI_PauseMenu pauseMenu;
    [SerializeField] UI_WinScreen winScreen;
    [SerializeField] UI_Controls controls;
    [SerializeField] UI_Settings settings;
    [SerializeField] GameObject deathNote;

    public static UI_Ammo Ammo => Instance.ammo;
    public static UI_HealthBar HealthBar => Instance.healthBar;
    public static UI_CoinAmount CoinAmount => Instance.coinAmount;
    public static A_UI Audio => Instance.sound;
    public static UI_Crosshair Crosshair => Instance.crosshair;
    public static UI_PauseMenu PauseMenu => Instance.pauseMenu;
    public static UI_WinScreen WinScreen => Instance.winScreen;
    public static UI_Controls Controls => Instance.controls;
    public static UI_Settings Settings => Instance.settings;
    public static GameObject DeathNote => Instance.deathNote;

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
        pauseMenu.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        deathNote.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TogglePaused()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            ShowHUD(false);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.gameObject.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            ShowHUD(true);
        }
    }

    public static void ShowHUD(bool show = true)
    {
        Instance.crosshair.gameObject.SetActive(show);
        Instance.healthBar.gameObject.SetActive(show);
        Instance.ammo.gameObject.SetActive(show);
        Instance.coinAmount.gameObject.SetActive(show);
    }
}
