// IllnessDetail.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IllnessDetail : MonoBehaviour
{
    [Header("Panel UI Elements")]
    [SerializeField] private Image _illnessImage;
    [SerializeField] private TextMeshProUGUI _illnessNameText;
    [SerializeField] private TextMeshProUGUI _illnessDescriptionText;

    /// <summary>
    /// Populates the detail panel with the given illness data.
    /// Animation is handled by IllnessDexNavigation via IllnessDetailAnimation.
    /// </summary>
    public void ShowDetail(IllnessData data)
    {
        PopulatePanel(data);
    }

    /// <summary>
    /// Fills the panel UI elements with the illness name,
    /// description and colored sprite.
    /// </summary>
    private void PopulatePanel(IllnessData data)
    {
        _illnessNameText.text = data.IllnessName;
        _illnessDescriptionText.text = data.Description;
        _illnessImage.sprite = data.UnlockedSprite;
    }
}