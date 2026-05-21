using DG.Tweening;
using UnityEngine;

public class PanelSlideAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private RectTransform _panelRect;

    [Header("Animation Settings")]
    [SerializeField] private float _panelHiddenPosX;
    [SerializeField] private float _panelShownPosX;
    [SerializeField] private float _panelTweenDuration;

    private void Awake()
    {
        HidePanelInstant();
    }

    /// <summary>
    /// Snaps the panel off screen instantly on startup without animation.
    /// </summary>
    private void HidePanelInstant()
    {
        _panel.SetActive(false);
        _panelRect.anchoredPosition = new Vector2(
            _panelHiddenPosX, _panelRect.anchoredPosition.y);
    }

    /// <summary>
    /// Activates the panel and slides it into view.
    /// </summary>
    public void ShowPanel()
    {
        _panel.SetActive(true);
        _panelRect.DOAnchorPosX(_panelShownPosX, _panelTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Slides the panel off screen and deactivates it once finished.
    /// </summary>
    public async void HidePanel()
    {
        await _panelRect
            .DOAnchorPosX(_panelHiddenPosX, _panelTweenDuration)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();

        _panel.SetActive(false);
    }
}