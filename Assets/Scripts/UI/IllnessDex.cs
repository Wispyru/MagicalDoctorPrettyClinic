using UnityEngine;
using UnityEngine.UI;

public class IllnessDex : MonoBehaviour
{
    [Header("Illness Data")]
    [SerializeField] private IllnessData[] _illnessData;

    [Header("Illness Buttons")]
    [SerializeField] private Button[] _illnessButtons;

    private IllnessDetail _illnessDetail;
    private IllnessDexNavigation _illnessDexNavigation;

    private void Awake()
    {
        _illnessDetail = GetComponent<IllnessDetail>();
        _illnessDexNavigation = GetComponent<IllnessDexNavigation>();
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
    /// Assigns the correct sprite to the button image and registers
    /// the click listener. Locked buttons show the silhouette,
    /// have no hover highlight and do nothing on click.
    /// </summary>
    private void SetupButton(Button button, IllnessData data, int index)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
            buttonImage.sprite = data.IsUnlocked ? data.UnlockedSprite : data.LockedSprite;

        button.onClick.RemoveAllListeners();

        if (data.IsUnlocked)
        {
            SetButtonUnlocked(button, index);
        }
        else
        {
            SetButtonLocked(button);
        }
    }

    /// <summary>
    /// Enables the button and restores color tint transition and adds the click listener.
    /// </summary>
    private void SetButtonUnlocked(Button button, int index)
    {
        button.interactable = true;
        button.transition = Selectable.Transition.ColorTint;
        int captured = index;
        button.onClick.AddListener(() => OnIllnessButtonClicked(captured));
    }

    /// <summary>
    /// Disables the button interaction and removes the color tint transition
    /// so locked buttons have no hover highlight.
    /// </summary>
    private void SetButtonLocked(Button button)
    {
        button.interactable = false;
        button.transition = Selectable.Transition.None;
    }

    /// <summary>
    /// Called when an unlocked illness button is clicked.
    /// Passes the illness data to the detail panel and tells
    /// navigation to switch panels.
    /// </summary>
    private void OnIllnessButtonClicked(int index)
    {
        _illnessDetail.ShowDetail(_illnessData[index]);
        _illnessDexNavigation.OpenDetail();
    }
}