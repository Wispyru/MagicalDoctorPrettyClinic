using UnityEngine;
using UnityEngine.UI;

public class IllnessDex : MonoBehaviour
{
    [Header("Illness Data")]
    [SerializeField] private IllnessData[] _illnessData;

    [Header("Illness Buttons")]
    [SerializeField] private Button[] _illnessButtons;

    private IllnessDetail _illnessDetail;

    private void Awake()
    {
        _illnessDetail = GetComponent<IllnessDetail>();
        SetupIllnessButtons();
    }

    /// <summary>
    /// Loops through all illness buttons and sets up their sprite
    /// and click behaviour based on whether they are unlocked.
    /// </summary>
    private void SetupIllnessButtons()
    {
        for (int i = 0; i < _illnessButtons.Length; i++)
        {
            if (i >= _illnessData.Length)
                return;

            SetupButton(_illnessButtons[i], _illnessData[i], i);
        }
    }

    /// <summary>
    /// Assigns the correct sprite to the button image and registers the click listener.
    /// Unlocked buttons show the colored sprite and open the detail panel.
    /// Locked buttons show the silhouette and do nothing on click.
    /// </summary>
    private void SetupButton(Button button, IllnessData data, int index)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
            buttonImage.sprite = data.IsUnlocked ? data.UnlockedSprite : data.LockedSprite;

        button.onClick.RemoveAllListeners();

        if (data.IsUnlocked)
        {
            int captured = index;
            button.onClick.AddListener(() => OnIllnessButtonClicked(captured));
        }
    }

    /// <summary>
    /// Called when an unlocked illness button is clicked.
    /// Passes the matching IllnessData to the detail panel.
    /// </summary>
    private void OnIllnessButtonClicked(int index)
    {
        _illnessDetail.ShowDetail(_illnessData[index]);
    }
}