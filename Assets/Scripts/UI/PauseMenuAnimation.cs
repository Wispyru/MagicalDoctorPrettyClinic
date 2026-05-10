// PauseMenuAnimation.cs
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class PauseMenuAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private RectTransform _pausePanelRect;
    [SerializeField] private CanvasGroup _overlayCanvasGroup;

    [Header("Button References")]
    [SerializeField] private RectTransform _pauseButtonRect;

    [Header("Panel Positions")]
    [SerializeField] private float _panelHiddenPosY;
    [SerializeField] private float _panelShownPosY;

    [Header("Button Positions")]
    [SerializeField] private float _buttonHiddenPosY;
    [SerializeField] private float _buttonShownPosY;

    [Header("Durations")]
    [SerializeField] private float _panelTweenDuration;
    [SerializeField] private float _buttonTweenDuration;

    /// <summary>
    /// Plays the pause menu intro by fading the overlay in and
    /// sliding the panel and button into view simultaneously.
    /// </summary>
    public void PlayIntro()
    {
        FadeOverlay(1f);
        SlidePanel(_panelShownPosY);
        SlideButton(_buttonShownPosY);
    }

    /// <summary>
    /// Plays the pause menu outro and waits for all animations to complete
    /// before returning so PauseMenu knows when to deactivate the panel.
    /// </summary>
    public async Task PlayOutro()
    {
        FadeOverlay(0f);
        await SlidePanel(_panelHiddenPosY);
        await SlideButton(_buttonHiddenPosY);
    }

    /// <summary>
    /// Fades the dark overlay to the target alpha.
    /// SetUpdate(true) keeps the animation running while Time.timeScale is 0.
    /// </summary>
    private void FadeOverlay(float targetAlpha)
    {
        _overlayCanvasGroup.DOFade(targetAlpha, _panelTweenDuration).SetUpdate(true);
    }

    /// <summary>
    /// Slides the pause panel to the target Y position.
    /// Returns a Task so the caller can await it.
    /// </summary>
    private Task SlidePanel(float targetY)
    {
        return _pausePanelRect
            .DOAnchorPosY(targetY, _panelTweenDuration)
            .SetUpdate(true)
            .AsyncWaitForCompletion();
    }

    /// <summary>
    /// Slides the pause button to the target Y position.
    /// Returns a Task so the caller can await it.
    /// </summary>
    private Task SlideButton(float targetY)
    {
        return _pauseButtonRect
            .DOAnchorPosY(targetY, _buttonTweenDuration)
            .SetUpdate(true)
            .AsyncWaitForCompletion();
    }
}