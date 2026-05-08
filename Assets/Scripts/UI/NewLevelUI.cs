// NewLevelUI.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelUI : MonoBehaviour
{
    /// <summary>
    /// Unlocks the next level if the current scene is beyond the previously reached index.
    /// </summary>
    public void UnlockNewLevel()
    {
        if (IsNewLevelReached())
        {
            SaveProgress();
        }
    }

    /// <summary>
    /// Checks whether the player has gone further than their saved progress.
    /// </summary>
    private bool IsNewLevelReached()
    {
        return SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex");
    }

    /// <summary>
    /// Saves the newly reached level index and increments the unlocked level count.
    /// </summary>
    private void SaveProgress()
    {
        int currentUnlocked = PlayerPrefs.HasKey("UnlockedLevel")
            ? PlayerPrefs.GetInt("UnlockedLevel")
            : 1;

        PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("UnlockedLevel", currentUnlocked + 1);
        PlayerPrefs.Save();
    }
}