using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StartScreen : MonoBehaviour
{
    public void OnStartPress()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}
