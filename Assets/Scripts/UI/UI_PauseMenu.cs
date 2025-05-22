using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    public void OnResumePress()
    {
        UI.TogglePaused();
    }

    public void OnResetPress()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void OnSettingsPress()
    {
        UI.Settings.gameObject.SetActive(true);
    }

    public void OnControlsPress()
    {
        UI.Controls.gameObject.SetActive(true);
    }

    public void OnExitPress()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
