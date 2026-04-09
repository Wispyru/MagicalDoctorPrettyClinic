using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void SettingsScreen()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Index()
    {
        SceneManager.LoadSceneAsync(3);
    }
}
