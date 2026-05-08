// LevelButtonAnimation.cs
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonAnimation : MonoBehaviour
{
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private float _punchScale;
    [SerializeField] private float _punchDuration;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibrato;

    private HashSet<RectTransform> _animatingButtons = new HashSet<RectTransform>();

    private void Start()
    {
        RegisterHoverListeners();
    }

    /// <summary>
    /// Kills all active tweens on destroy to prevent callbacks firing on destroyed objects.
    /// </summary>
    private void OnDestroy()
    {
        foreach (RectTransform rectTransform in _animatingButtons)
        {
            if (rectTransform != null)
                rectTransform.DOKill();
        }
        _animatingButtons.Clear();
    }

    /// <summary>
    /// Adds a hover listener to each button using EventTrigger.
    /// Unlocked buttons play a punch, locked buttons play a shake.
    /// </summary>
    private void RegisterHoverListeners()
    {
        foreach (Button button in _levelButtons)
        {
            Button captured = button;
            EventTrigger trigger = GetOrAddEventTrigger(button);
            AddHoverEntry(trigger, captured);
        }
    }

    /// <summary>
    /// Gets the existing EventTrigger on a button or adds one if missing.
    /// </summary>
    private EventTrigger GetOrAddEventTrigger(Button button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        return trigger;
    }

    /// <summary>
    /// Adds a PointerEnter entry to the EventTrigger that plays the correct animation.
    /// </summary>
    private void AddHoverEntry(EventTrigger trigger, Button button)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((_) => OnButtonHovered(button));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// Checks if the button's CanvasGroup allows interaction to decide which animation to play.
    /// Ignores the hover entirely if the button is already animating.
    /// </summary>
    private void OnButtonHovered(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        if (_animatingButtons.Contains(rectTransform))
            return;

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        bool isUnlocked = canvasGroup == null || canvasGroup.interactable;

        if (isUnlocked)
        {
            PlayUnlockedAnimation(rectTransform);
        }
        else
        {
            PlayLockedAnimation(rectTransform);
        }
    }

    /// <summary>
    /// Punches the button scale outward when hovered to invite the player to click.
    /// Registers the button as animating and clears it when done.
    /// </summary>
    private void PlayUnlockedAnimation(RectTransform rectTransform)
    {
        _animatingButtons.Add(rectTransform);
        rectTransform.DOPunchScale(Vector3.one * _punchScale, _punchDuration)
            .OnComplete(() =>
            {
                if (rectTransform != null)
                    _animatingButtons.Remove(rectTransform);
            });
    }

    /// <summary>
    /// Shakes the button rotation when hovered to signal it cannot be played.
    /// Registers the button as animating and clears it when done.
    /// </summary>
    private void PlayLockedAnimation(RectTransform rectTransform)
    {
        _animatingButtons.Add(rectTransform);
        rectTransform.DOShakeRotation(_shakeDuration, new Vector3(0, 0, _shakeStrength), _shakeVibrato)
            .OnComplete(() =>
            {
                if (rectTransform != null)
                    _animatingButtons.Remove(rectTransform);
            });
    }
}