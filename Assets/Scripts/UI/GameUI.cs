using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _scoreTexts;

    [SerializeField]
    private Image[] _scoreIcons;

    [SerializeField]
    private TextMeshProUGUI _comboText;

    [SerializeField]
    private Sprite[] _medicineSprites;

    [SerializeField]
    private float _comboFadeDelay = 3f;

    [SerializeField]
    private float _comboFadeDuration = 2f;

    private Tween _comboFadeTween;

    private void Start()
    {
        SetupScoreIcons();
        ResetComboText();
    }

    /// <summary>
    /// Assigns the correct medicine sprite to each score icon.
    /// </summary>
    private void SetupScoreIcons()
    {
        for (int i = 0; i < _scoreIcons.Length; i++)
        {
            if (i < _medicineSprites.Length)
                _scoreIcons[i].sprite = _medicineSprites[i];
        }
    }

    /// <summary>
    /// Updates all UI elements to reflect the current GameData state.
    /// </summary>
    public void UpdateUI()
    {
        UpdateScoreDisplays();
        UpdateComboDisplay();
    }

    /// <summary>
    /// Updates each medicine type score text.
    /// </summary>
    private void UpdateScoreDisplays()
    {
        for (int i = 0; i < _scoreTexts.Length; i++)
        {
            if (i < GameData.ScorePerType.Length)
                _scoreTexts[i].text = GameData.ScorePerType[i].ToString();
        }
    }

    private void UpdateComboDisplay()
    {
        if (!GameData.IsComboActive)
        {
            ResetComboText();
            return;
        }

        _comboText.text = $"x{GameData.CurrentComboCount} COMBO!";
        _comboText.color = new Color(_comboText.color.r, _comboText.color.g, _comboText.color.b, 1f);

        if (_comboFadeTween != null)
            _comboFadeTween.Kill();

        _comboFadeTween = _comboText.DOFade(0f, _comboFadeDuration)
            .SetDelay(_comboFadeDelay);
    }

    /// <summary>
    /// Hides the combo text and kills any active fade tween.
    /// </summary>
    private void ResetComboText()
    {
        if (_comboFadeTween != null)
            _comboFadeTween.Kill();

        _comboText.color = new Color(_comboText.color.r, _comboText.color.g, _comboText.color.b, 0f);
    }
}