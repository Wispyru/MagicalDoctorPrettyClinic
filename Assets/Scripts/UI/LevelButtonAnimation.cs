// LevelButtonAnimation.cs
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonAnimation : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button[] _levelButtons;

    [Header("Unlock Animation Settings")]
    [SerializeField] private float _punchScale;
    [SerializeField] private float _punchDuration;

    [Header("Lock Animation Settings")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibrato;

    private HashSet<RectTransform> _animatingButtons = new HashSet<RectTransform>();

    private void Awake()
    {
        RegisterHoverListeners();
    }

    /// <summary>
    /// Kills all active tweens when the GameObject is destroyed
    /// to prevent OnComplete callbacks firing on destroyed objects.
    /// </summary>
    private void OnDestroy()
    {
        StopAllButtonAnimations();
    }

    /// <summary>
    /// Loops through all level buttons and adds a PointerEnter
    /// EventTrigger to each one.
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
    /// Returns the EventTrigger on a button, adding one if it doesn't exist.
    /// </summary>
    private EventTrigger GetOrAddEventTrigger(Button button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        return trigger;
    }

    /// <summary>
    /// Adds a PointerEnter entry to the EventTrigger that fires OnButtonHovered.
    /// </summary>
    private void AddHoverEntry(EventTrigger trigger, Button button)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((_) => OnButtonHovered(button));
        trigger.triggers.Add(entry);
    }

    /// <summary>
    /// Fires when the player hovers over a button.
    /// Reads the CanvasGroup to check if locked and plays the correct animation.
    /// Ignores the hover if the button is already animating.
    /// </summary>
    private void OnButtonHovered(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        if (_animatingButtons.Contains(rectTransform))
            return;

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        bool isUnlocked = canvasGroup == null || canvasGroup.interactable;

        if (isUnlocked)
            PlayUnlockedAnimation(rectTransform);
        else
            PlayLockedAnimation(rectTransform);
    }

    /// <summary>
    /// Punches the button scale outward on hover.
    /// </summary>
    private void PlayUnlockedAnimation(RectTransform rectTransform)
    {
        PlayAnimation(rectTransform,
            rectTransform.DOPunchScale(Vector3.one * _punchScale, _punchDuration));
    }

    /// <summary>
    /// Shakes the button rotation on hover to signal it is locked.
    /// </summary>
    private void PlayLockedAnimation(RectTransform rectTransform)
    {
        PlayAnimation(rectTransform,
            rectTransform.DOShakeRotation(_shakeDuration,
                new Vector3(0, 0, _shakeStrength), _shakeVibrato));
    }

    /// <summary>
    /// Registers a button as animating and removes it from the set
    /// once the tween completes. Shared by all animation types.
    /// </summary>
    private void PlayAnimation(RectTransform rectTransform, Tween tween)
    {
        _animatingButtons.Add(rectTransform);
        tween.OnComplete(() =>
        {
            if (rectTransform != null)
                _animatingButtons.Remove(rectTransform);
        });
    }

    /// <summary>
    /// Kills all active tweens on all currently animating buttons.
    /// </summary>
    private void StopAllButtonAnimations()
    {
        foreach (RectTransform rectTransform in _animatingButtons)
        {
            if (rectTransform != null)
                rectTransform.DOKill();
        }
        _animatingButtons.Clear();
    }
}