using UnityEngine;
using UnityEngine.SceneManagement;

public class DeletePlayerPrefs : MonoBehaviour
{
    [SerializeField] private int _mainMenuSceneIndex = 0;

    /// <summary>
    /// Deletes all PlayerPrefs data, saves to disk, then returns to the main menu.
    /// </summary>
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // delete later?
        LoadMainMenu();
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneIndex);
    }
}