// MainMenuAnimation.cs
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform[] _menuButtonRects;
    [SerializeField] private float _startPosY;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private float _delayBetweenButtons;
    [SerializeField] private float[] _endPositionsY; // Each button gets its own end position

    private void Start()
    {
        AnimateButtonsIn();
    }

    /// <summary>
    /// Moves all buttons off screen instantly, then drops them in one by one with a stagger delay.
    /// </summary>
    private async void AnimateButtonsIn()
    {
        SetButtonsOffScreen();
        await DropButtonsIn();
    }

    /// <summary>
    /// Moves all buttons to the start position above the screen before animating.
    /// </summary>
    private void SetButtonsOffScreen()
    {
        foreach (RectTransform button in _menuButtonRects)
        {
            button.DOAnchorPosY(_startPosY, 0f);
        }
    }

    /// <summary>
    /// Drops each button down to its own end position one by one with a stagger.
    /// </summary>
    private async Task DropButtonsIn()
    {
        for (int i = 0; i < _menuButtonRects.Length; i++)
        {
            float targetY = i < _endPositionsY.Length ? _endPositionsY[i] : 0f;
            _menuButtonRects[i].DOAnchorPosY(targetY, _tweenDuration).SetEase(Ease.OutBounce);
            await Task.Delay((int)(_delayBetweenButtons * 1000));
        }
    }
}