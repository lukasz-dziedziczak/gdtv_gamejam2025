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
        SceneManager.LoadScene(1);
    }

    public void OnSettingsPress()
    {

    }

    public void OnExitPress()
    {
        SceneManager.LoadScene(0);
    }
}
