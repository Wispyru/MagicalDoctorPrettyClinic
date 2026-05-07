// DebugLevelSystem.cs
// Add this to your project for testing the popup system and locked buttons

using UnityEngine;

public class DebugLevelSystem : MonoBehaviour
{
    [Header("Testing Options")]
    [SerializeField] private bool _showDebugPanel = true;
    private KeyCode _toggleDebugKey = KeyCode.D;

    private bool _debugPanelActive = true;
    private Vector2 _scrollPosition = Vector2.zero;

    private void Update()
    {
        if (Input.GetKeyDown(_toggleDebugKey))
        {
            _debugPanelActive = !_debugPanelActive;
            Debug.Log(_debugPanelActive ? "Debug panel ON (press D to close)" : "Debug panel OFF (press D to open)");
        }
    }

    private void OnGUI()
    {
        if (!_showDebugPanel || !_debugPanelActive) return;

        // Draw debug panel on left side - Optimized for portrait 1080x2400
        GUILayout.BeginArea(new Rect(10, 10, 500, Screen.height - 20));

        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

        GUILayout.Label("=== DEBUG CONSOLE ===", GUI.skin.box);
        GUILayout.Label("Press 'D' to toggle panel", GUI.skin.label);
        GUILayout.Space(15);

        GUILayout.Label("--- UNLOCK/LOCK ---", GUI.skin.box);
        if (GUILayout.Button("✓ Unlock All Levels", GUILayout.Height(70)))
        {
            UnlockAllLevels();
        }

        if (GUILayout.Button("🔒 Lock All Levels", GUILayout.Height(70)))
        {
            LockAllLevels();
        }

        if (GUILayout.Button("🔓 Unlock Level 2 Only", GUILayout.Height(70)))
        {
            UnlockSpecificLevel(2);
        }

        GUILayout.Space(15);
        GUILayout.Label("--- SCORE TESTING (6.4) ---", GUI.skin.box);

        if (GUILayout.Button("★☆☆ Level 1: 150 (1 star)", GUILayout.Height(70)))
        {
            SetScore("Level1", 150);
        }

        if (GUILayout.Button("★★☆ Level 1: 300 (2 stars)", GUILayout.Height(70)))
        {
            SetScore("Level1", 300);
        }

        if (GUILayout.Button("★★★ Level 1: 650 (3 stars)", GUILayout.Height(70)))
        {
            SetScore("Level1", 650);
        }

        if (GUILayout.Button("☆☆☆ Clear Level 1 (0 stars)", GUILayout.Height(70)))
        {
            ClearScore("Level1");
        }

        GUILayout.Space(15);

        if (GUILayout.Button("★★★ Level 2: 900 (3 stars)", GUILayout.Height(70)))
        {
            SetScore("Level2", 900);
        }

        if (GUILayout.Button("🗑️ Clear All Scores", GUILayout.Height(70)))
        {
            ClearAllScores();
        }

        GUILayout.Space(15);
        GUILayout.Label("--- CURRENT STATUS ---", GUI.skin.box);
        GUILayout.Label(GetCurrentStatus(), GUI.skin.box);

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 999);
        PlayerPrefs.Save();
        Debug.Log("✓ All levels unlocked!");
    }

    private void LockAllLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
        Debug.Log("✓ All levels locked (only Level 1 unlocked)");
    }

    private void UnlockSpecificLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("UnlockedLevel", levelNumber);
        PlayerPrefs.Save();
        Debug.Log($"✓ Unlocked up to Level {levelNumber}");
    }

    private void SetScore(string levelName, int score)
    {
        PlayerPrefs.SetInt($"Score_{levelName}", score);
        PlayerPrefs.Save();
        Debug.Log($"✓ Set {levelName} score to {score}");
    }

    private void ClearScore(string levelName)
    {
        PlayerPrefs.DeleteKey($"Score_{levelName}");
        PlayerPrefs.Save();
        Debug.Log($"✓ Cleared {levelName} score");
    }

    private void ClearAllScores()
    {
        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.DeleteKey($"Score_Level{i}");
        }
        PlayerPrefs.Save();
        Debug.Log("✓ All scores cleared");
    }

    private string GetCurrentStatus()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int score1 = PlayerPrefs.GetInt("Score_Level1", 0);
        int score2 = PlayerPrefs.GetInt("Score_Level2", 0);

        return $"Unlocked Level: {unlockedLevel}\n" +
               $"Level 1 Score: {score1}\n" +
               $"Level 2 Score: {score2}";
    }
}
