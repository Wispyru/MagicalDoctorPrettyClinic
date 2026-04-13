using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true); // activate pause menu
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenuScene"); // go to main menu scene
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); // deactivate pause menu
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload active scene
        Time.timeScale = 1;
    }
}
