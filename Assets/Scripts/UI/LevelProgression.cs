using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    private const string UnlockedLevelKey = "UnlockedLevel";
    private const string SelectedLevelKey = "SelectedLevel";

    /// <summary>
    /// Reads which level was just completed from PlayerPrefs,
    /// then unlocks the next level if it hasn't been unlocked yet.
    /// Call this via Unity Events on the win screen return button
    /// before SwapScene loads the next scene.
    /// </summary>
    public void UnlockNextLevel()
    {
        int completedLevelId = GetCompletedLevelId();
        int currentUnlocked = PlayerPrefs.GetInt(UnlockedLevelKey, 1);

        if (IsNewLevelReached(completedLevelId, currentUnlocked))
        {
            SaveProgress(currentUnlocked);
        }
    }

    /// <summary>
    /// Returns the level ID that was just completed by reading
    /// the SelectedLevel key saved by TemporaryLevelSelect.
    /// </summary>
    private int GetCompletedLevelId()
    {
        int id = PlayerPrefs.GetInt(SelectedLevelKey, 1);
        Debug.Log("Completed level ID read: " + id);
        return id;
    }

    /// <summary>
    /// Checks if the completed level unlocks a level beyond
    /// what is currently saved, preventing duplicate unlocks.
    /// </summary>
    private bool IsNewLevelReached(int completedLevelId, int currentUnlocked)
    {
        return completedLevelId + 1 > currentUnlocked;
    }

    /// <summary>
    /// Increments and saves the unlocked level count to PlayerPrefs.
    /// </summary>
    private void SaveProgress(int currentUnlocked)
    {
        PlayerPrefs.SetInt(UnlockedLevelKey, currentUnlocked + 1);
        PlayerPrefs.Save();
    }
}