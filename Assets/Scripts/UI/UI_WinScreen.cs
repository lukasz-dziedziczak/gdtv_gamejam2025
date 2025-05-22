using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WinScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnResetPress()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void OnExitPress()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

}
