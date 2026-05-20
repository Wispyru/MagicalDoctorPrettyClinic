using DG.Tweening;
using UnityEngine;

public class IllnessDexAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject _dexPanel;
    [SerializeField] private RectTransform _dexPanelRect;

    [Header("Animation Settings")]
    [SerializeField] private float _panelHiddenPosX;
    [SerializeField] private float _panelShownPosX;
    [SerializeField] private float _panelTweenDuration;

    private void Awake()
    {
        HidePanelInstant();
    }

    /// <summary>
    /// Snaps the illness panel off screen instantly on startup.
    /// Uses Start instead of Awake so it runs even if the parent
    /// starts inactive, and only hides the child panel not the parent.
    /// </summary>
    private void HidePanelInstant()
    {
        _dexPanel.SetActive(false);
        _dexPanelRect.anchoredPosition = new Vector2(
            _panelHiddenPosX, _dexPanelRect.anchoredPosition.y);
    }

    /// <summary>
    /// Activates the illness panel and slides it in from the right.
    /// </summary>
    public void ShowPanel()
    {
        _dexPanel.SetActive(true);
        _dexPanelRect.DOAnchorPosX(_panelShownPosX, _panelTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Slides the illness panel back to the right and deactivates it
    /// once the animation finishes.
    /// </summary>
    public async void HidePanel()
    {
        await _dexPanelRect
            .DOAnchorPosX(_panelHiddenPosX, _panelTweenDuration)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();

        _dexPanel.SetActive(false);
    }
}