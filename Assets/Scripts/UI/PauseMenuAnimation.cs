using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class PauseMenuAnimation : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private RectTransform _pausePanelRect;
    [SerializeField] private CanvasGroup _overlayCanvasGroup;

    [Header("Panel Positions")]
    [SerializeField] private float _panelHiddenPosY;
    [SerializeField] private float _panelShownPosY;

    [Header("Durations")]
    [SerializeField] private float _panelTweenDuration;

    /// <summary>
    /// Fades the overlay in and slides the pause panel into view.
    /// </summary>
    public void PlayIntro()
    {
        FadeOverlay(1f);
        SlidePanel(_panelShownPosY);
    }

    /// <summary>
    /// Fades the overlay out and slides the pause panel away.
    /// Awaited by PauseMenu.Resume() so it waits for the animation to finish.
    /// </summary>
    public async Task PlayOutro()
    {
        FadeOverlay(0f);
        await SlidePanel(_panelHiddenPosY);
    }

    /// <summary>
    /// Fades the dark overlay to the target alpha.
    /// SetUpdate(true) keeps it running while Time.timeScale is 0.
    /// </summary>
    private void FadeOverlay(float targetAlpha)
    {
        _overlayCanvasGroup.DOFade(targetAlpha, _panelTweenDuration).SetUpdate(true);
    }

    /// <summary>
    /// Slides the pause panel to the target Y position and returns a Task
    /// so the caller can await completion.
    /// </summary>
    private Task SlidePanel(float targetY)
    {
        return _pausePanelRect
            .DOAnchorPosY(targetY, _panelTweenDuration)
            .SetUpdate(true)
            .AsyncWaitForCompletion();
    }
}