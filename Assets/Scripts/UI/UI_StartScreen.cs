using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
    [SerializeField] UI_Settings settings;
    [SerializeField] UI_Controls controls;

    private void OnEnable()
    {
        settings.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
    }

    public void OnStartPress()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsPress()
    {
        settings.gameObject.SetActive(true);
    }

    public void OnControlsPress()
    {
        controls.gameObject.SetActive(true);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}
