using UnityEngine;

public class IllnessDexNavigation : MonoBehaviour
{
    private IllnessDexAnimation _illnessDexAnimation;
    private IllnessDetailAnimation _illnessDetailAnimation;
    private CanvasGroup _canvasGroup;

    private PanelState _currentState = PanelState.Closed;

    private void Awake()
    {
        _illnessDexAnimation = GetComponent<IllnessDexAnimation>();
        _illnessDetailAnimation = GetComponent<IllnessDetailAnimation>();
        _canvasGroup = GetComponent<CanvasGroup>();
        HideContainer();
    }

    /// <summary>
    /// Makes the entire Illness Dex invisible and unclickable without
    /// deactivating the GameObject so all scripts stay initialized.
    /// </summary>
    private void HideContainer()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    /// <summary>
    /// Makes the entire Illness Dex visible and interactable.
    /// </summary>
    private void ShowContainer()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    /// <summary>
    /// Opens the illness dex from the level select.
    /// Called by the illness dex button via Unity Events.
    /// </summary>
    public void OpenDex()
    {
        ShowContainer();
        _currentState = PanelState.DexOpen;
        _illnessDexAnimation.ShowPanel();
    }

    /// <summary>
    /// Opens the detail panel and hides the dex panel simultaneously.
    /// Called by IllnessDex when an unlocked illness button is clicked.
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
    /// Hides the detail panel and shows the dex panel simultaneously.
    /// </summary>
    private void ReturnToDex()
    {
        _currentState = PanelState.DexOpen;
        _illnessDetailAnimation.HidePanel();
        _illnessDexAnimation.ShowPanel();
    }

    /// <summary>
    /// Hides the dex panel and makes the entire container invisible again.
    /// </summary>
    private void CloseDex()
    {
        _currentState = PanelState.Closed;
        _illnessDexAnimation.HidePanel();
        HideContainer();
    }
}