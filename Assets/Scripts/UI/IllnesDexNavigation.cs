using UnityEngine;

public class IllnessDexNavigation : MonoBehaviour
{
    private IllnessDexAnimation _illnessDexAnimation;
    private IllnessDetailAnimation _illnessDetailAnimation;

    private PanelState _currentState = PanelState.Closed;

    private void Awake()
    {
        _illnessDexAnimation = GetComponent<IllnessDexAnimation>();
        _illnessDetailAnimation = GetComponent<IllnessDetailAnimation>();
    }

    /// <summary>
    /// Opens the illness dex from the level select.
    /// Called by the illness dex button in the level select via Unity Events.
    /// </summary>
    public void OpenDex()
    {
        _currentState = PanelState.DexOpen;
        _illnessDexAnimation.ShowPanel();
    }

    /// <summary>
    /// Opens the detail panel and hides the dex at the same time.
    /// Called by IllnessDex when an illness button is clicked.
    /// </summary>
    public void OpenDetail()
    {
        _currentState = PanelState.DetailOpen;
        _illnessDexAnimation.HidePanel();
        _illnessDetailAnimation.ShowPanel();
    }

    /// <summary>
    /// Single back button handler that checks the current state
    /// and navigates to the correct previous screen.
    /// </summary>
    public void OnBackButtonClicked()
    {
        if (_currentState == PanelState.DetailOpen)
            ReturnToDex();
        else if (_currentState == PanelState.DexOpen)
            CloseDex();
    }

    /// <summary>
    /// Hides the detail panel and shows the dex again simultaneously.
    /// </summary>
    private void ReturnToDex()
    {
        _currentState = PanelState.DexOpen;
        _illnessDetailAnimation.HidePanel();
        _illnessDexAnimation.ShowPanel();
    }

    /// <summary>
    /// Closes the dex entirely and returns to the level select.
    /// </summary>
    private void CloseDex()
    {
        _currentState = PanelState.Closed;
        _illnessDexAnimation.HidePanel();
    }
}