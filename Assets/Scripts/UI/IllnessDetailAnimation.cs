using DG.Tweening;
using UnityEngine;

public class IllnessDetailAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject _detailPanel;
    [SerializeField] private RectTransform _detailPanelRect;

    [Header("Animation Settings")]
    [SerializeField] private float _panelHiddenPosX;
    [SerializeField] private float _panelShownPosX;
    [SerializeField] private float _panelTweenDuration;

    private void Awake()
    {
        HidePanelInstant();
    }

    /// <summary>
    /// Snaps the detail panel off screen instantly on startup.
    /// Uses Start instead of Awake so it runs correctly regardless
    /// of whether the parent starts active or inactive.
    /// </summary>
    private void HidePanelInstant()
    {
        _detailPanel.SetActive(false);
        _detailPanelRect.anchoredPosition = new Vector2(
            _panelHiddenPosX, _detailPanelRect.anchoredPosition.y);
    }

    /// <summary>
    /// Activates the detail panel and slides it in from the left.
    /// </summary>
    public void ShowPanel()
    {
        _detailPanel.SetActive(true);
        _detailPanelRect.DOAnchorPosX(_panelShownPosX, _panelTweenDuration)
            .SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// Slides the detail panel back to the left and deactivates it
    /// once the animation finishes.
    /// </summary>
    public async void HidePanel()
    {
        await _detailPanelRect
            .DOAnchorPosX(_panelHiddenPosX, _panelTweenDuration)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();

        _detailPanel.SetActive(false);
    }
}