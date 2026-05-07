using DG.Tweening;
using UnityEngine;

public class IllnessDetailAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject _detailPanel;
    [SerializeField] private RectTransform _detailPanelRect;

    [Header("Animation Settings")]
    [SerializeField] private float _panelHiddenPosY;
    [SerializeField] private float _panelShownPosY;
    [SerializeField] private float _panelTweenDuration;

    private void Awake()
    {
        HidePanelInstant();
    }

    /// <summary>
    /// Snaps the panel off screen instantly on startup without animation
    /// so it is ready to slide in when needed.
    /// </summary>
    private void HidePanelInstant()
    {
        _detailPanel.SetActive(false);
        _detailPanelRect.anchoredPosition = new Vector2(
            _detailPanelRect.anchoredPosition.x, _panelHiddenPosY);
    }

    /// <summary>
    /// Activates the panel and slides it up from the bottom into view.
    /// </summary>
    public void ShowPanel()
    {
        _detailPanel.SetActive(true);
        _detailPanelRect.DOAnchorPosY(_panelShownPosY, _panelTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Slides the panel back down off screen and deactivates it
    /// once the animation has finished.
    /// </summary>
    public async void HidePanel()
    {
        await _detailPanelRect
            .DOAnchorPosY(_panelHiddenPosY, _panelTweenDuration)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();

        _detailPanel.SetActive(false);
    }
}