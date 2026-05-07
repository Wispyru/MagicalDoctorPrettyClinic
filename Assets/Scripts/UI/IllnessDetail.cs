using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllnessDetail : MonoBehaviour
{
    [Header("Panel UI Elements")]
    [SerializeField] private Image _illnessImage;
    [SerializeField] private TextMeshProUGUI _illnessNameText;
    [SerializeField] private TextMeshProUGUI _illnessDescriptionText;

    private IllnessDetailAnimation _illnessDetailAnimation;

    private void Awake()
    {
        _illnessDetailAnimation = GetComponent<IllnessDetailAnimation>();
    }

    /// <summary>
    /// Populates the detail panel with the given illness data
    /// and triggers the slide-in animation.
    /// Called by IllnessDex when an unlocked illness is clicked.
    /// </summary>
    public void ShowDetail(IllnessData data)
    {
        PopulatePanel(data);
        _illnessDetailAnimation.ShowPanel();
    }

    /// <summary>
    /// Triggers the slide-out animation and hides the panel.
    /// Called by the back button via Unity Events in the Inspector.
    /// </summary>
    public void HideDetail()
    {
        _illnessDetailAnimation.HidePanel();
    }

    /// <summary>
    /// Fills the panel UI elements with the illness name, description and colored sprite.
    /// </summary>
    private void PopulatePanel(IllnessData data)
    {
        _illnessNameText.text = data.IllnessName;
        _illnessDescriptionText.text = data.Description;
        _illnessImage.sprite = data.UnlockedSprite;
    }
}