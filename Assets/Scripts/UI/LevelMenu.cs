// LevelMenu.cs
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button[] LevelButtons;

    [Header("Level Data")]
    [SerializeField] private TemporaryLevelData[] _TemporaryLevelData; // One entry per level, fill in Inspector

    [Header("Description Panel")]
    [SerializeField] private GameObject _descriptionPanel;
    [SerializeField] private RectTransform _descriptionPanelRect;
    [SerializeField] private float _panelHiddenPosY;   // Off screen position (e.g. -1200)
    [SerializeField] private float _panelShownPosY;    // On screen position (e.g. 0)
    [SerializeField] private float _panelTweenDuration;

    [Header("Panel UI Elements")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _minimumScoreText;
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _starsText;  // Displays ??? etc.
    [SerializeField] private Button _proceedButton;
    [SerializeField] private Button _exitButton;

    private float _lockedAlpha = 0.4f;
    private int _selectedLevelId = -1; // Tracks which level was clicked

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        SetupLevelButtons(unlockedLevel);
        HidePanelInstant();
    }

    /// <summary>
    /// Moves the panel off screen instantly without animation on startup.
    /// </summary>
    private void HidePanelInstant()
    {
        _descriptionPanel.SetActive(false);
        _descriptionPanelRect.anchoredPosition = new Vector2(
            _descriptionPanelRect.anchoredPosition.x, _panelHiddenPosY);
    }

    /// <summary>
    /// Enables unlocked level buttons and visually locks the rest.
    /// </summary>
    private void SetupLevelButtons(int unlockedLevel)
    {
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            bool isUnlocked = i < unlockedLevel;
            SetButtonState(LevelButtons[i], isUnlocked);
        }
    }

    /// <summary>
    /// Sets a button as locked or unlocked, updating its interactability,
    /// alpha transparency and lock icon visibility.
    /// </summary>
    private void SetButtonState(Button button, bool isUnlocked)
    {
        button.interactable = true;
        CanvasGroup buttonCanvasGroup = button.GetComponent<CanvasGroup>();
        if (buttonCanvasGroup == null)
            buttonCanvasGroup = button.gameObject.AddComponent<CanvasGroup>();

        buttonCanvasGroup.interactable = isUnlocked;
        buttonCanvasGroup.blocksRaycasts = true;

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color color = buttonImage.color;
            color.a = isUnlocked ? 1f : _lockedAlpha;
            buttonImage.color = color;
        }

        Transform lockIcon = button.transform.Find("LockIcon");
        if (lockIcon != null)
            lockIcon.gameObject.SetActive(!isUnlocked);
    }

    /// <summary>
    /// Called by each level button in the Inspector.
    /// Shows the description panel with the correct data for that level.
    /// </summary>
    public void OnLevelButtonClicked(int levelId)
    {
        CanvasGroup canvasGroup = LevelButtons[levelId - 1].GetComponent<CanvasGroup>();
        bool isUnlocked = canvasGroup == null || canvasGroup.interactable;

        if (!isUnlocked)
            return; // Locked levels only trigger the shake animation, not the panel

        _selectedLevelId = levelId;
        PopulatePanel(levelId - 1); // Array is 0-indexed, levelId starts at 1
        ShowPanel();
    }

    /// <summary>
    /// Fills all the UI elements in the panel with the correct level data.
    /// </summary>
    private void PopulatePanel(int index)
    {
        if (index < 0 || index >= _TemporaryLevelData.Length)
            return;

        TemporaryLevelData data = _TemporaryLevelData[index];

        _levelNameText.text = data.LevelName;
        _descriptionText.text = data.Description;
        _minimumScoreText.text = "Min Score: " + data.MinimumScore;

        int savedScore = PlayerPrefs.GetInt("Score_Level" + (index + 1), 0);
        _playerScoreText.text = "Best Score: " + savedScore;

        _starsText.text = CalculateStars(savedScore, data.MinimumScore);
    }

    /// <summary>
    /// Returns a star string based on the player's score vs the minimum score.
    /// 1 star = 30%, 2 stars = 60%, 3 stars = 100% of minimum score.
    /// </summary>
    private string CalculateStars(int score, int minimumScore)
    {
        float percentage = minimumScore > 0 ? (float)score / minimumScore * 100f : 0f;

        if (percentage >= 100f) return "???";
        if (percentage >= 60f) return "???";
        if (percentage >= 30f) return "???";
        return "???";
    }

    /// <summary>
    /// Activates and slides the description panel up from the bottom.
    /// </summary>
    private void ShowPanel()
    {
        _descriptionPanel.SetActive(true);
        _descriptionPanelRect.DOAnchorPosY(_panelShownPosY, _panelTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Slides the description panel back down and deactivates it when done.
    /// </summary>
    public async void HidePanel()
    {
        await _descriptionPanelRect
            .DOAnchorPosY(_panelHiddenPosY, _panelTweenDuration)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();

        _descriptionPanel.SetActive(false);
    }

    /// <summary>
    /// Loads the selected level scene when the proceed button is pressed.
    /// </summary>
    public void OpenLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }
}