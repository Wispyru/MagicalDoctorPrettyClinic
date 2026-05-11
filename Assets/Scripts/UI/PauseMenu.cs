using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Object")]
    [SerializeField] private GameObject _pauseMenuObject;

    private PauseMenuAnimation _pauseMenuAnimation;

    private void Awake()
    {
        _pauseMenuAnimation = GetComponent<PauseMenuAnimation>();
    }

    /// <summary>
    /// Activates the pause menu, stops time and plays the intro animation.
    /// </summary>
    public void Pause()
    {
        _pauseMenuObject.SetActive(true);
        Time.timeScale = 0;
        _pauseMenuAnimation.PlayIntro();
    }

    /// <summary>
    /// Plays the outro animation, deactivates the pause menu and restores time.
    /// </summary>
    public async void Resume()
    {
        await _pauseMenuAnimation.PlayOutro();
        _pauseMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Restores time and loads the main menu scene.
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    /// <summary>
    /// Restores time and reloads the current scene.
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}