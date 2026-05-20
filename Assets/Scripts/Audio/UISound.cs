using UnityEngine;

public class UISound : MonoBehaviour
{
    [Header("Button Sound")]
    [SerializeField] private AudioClip _clickSFX;

    [Header("Lock Settings")]
    [SerializeField] private bool _isLocked;

    /// <summary>
    /// Plays the assigned click SFX if the button is unlocked,
    /// or the universal locked SFX if the button is locked.
    /// Attach to any button and call this via OnClick in the Inspector.
    /// </summary>
    public void PlayButtonSound()
    {
        if (_isLocked)
            AudioManager.Instance.PlayLockedSFX();
        else
            AudioManager.Instance.PlaySFX(_clickSFX);
    }

    /// <summary>
    /// Updates the locked state from other scripts such as LevelSelect
    /// or IllnessDex when they determine a button should be locked.
    /// </summary>
    public void SetLocked(bool isLocked)
    {
        _isLocked = isLocked;
    }
}