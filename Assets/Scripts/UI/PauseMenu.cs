using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private RectTransform pausePanelRect;
    [SerializeField] private RectTransform pauseButtonRect;
    [SerializeField] private float _topPosPanelPauseY, _middlePosPanelPauseY;
    [SerializeField] private float _topPosButtonPauseX, _middlePosButtonPauseX;
    [SerializeField] private float _tweenDurationPanelPause;
    [SerializeField] private float _tweenDurationButtonPause;
    [SerializeField] private CanvasGroup canvasGroup; // Dark panel canvas group

    /// <summary>
    /// Pauses the game ad stops time
    /// </summary>
    public void Pause()
    {
        pauseMenu.SetActive(true); // activate pause menu
        Time.timeScale = 0;
        PausePanelIntro();
    }

    /// <summary>
    /// Go the main menu and resumes time
    /// </summary>
    public void Home()
    {
        SceneManager.LoadScene("MainMenuScene"); // go to main menu scene
        Time.timeScale = 1;
    }

    /// <summary>
    /// Resumes the time and continues the game
    /// </summary>
    public async void Resume()
    {
        await PausePanelOutro(); // pause menu WAITS for this fucntion to end until proceding
        pauseMenu.SetActive(false); // deactivate pause menu
        Time.timeScale = 1;
    }

    /// <summary>
    /// Reloads the entire scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload active scene
        Time.timeScale = 1;
    }

    /// <summary>
    /// manages the dark panels fade in, handles the DOTween animation duration and position when spwaning in
    /// </summary>
    private void PausePanelIntro()
    {
        canvasGroup.DOFade(1, _tweenDurationPanelPause).SetUpdate(true); // regulates the fade
        pausePanelRect.DOAnchorPosY(_middlePosPanelPauseY, _tweenDurationPanelPause).SetUpdate(true); // specify the target position and determine the time to reach set destination
        pauseButtonRect.DOAnchorPosY(_middlePosButtonPauseX, _tweenDurationButtonPause).SetUpdate(true);
    }

    /// <summary>
    /// manages the dark panels fade out, handles the DOTween animation duration and position when spwaning out of the scene
    /// </summary>
    private async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, _tweenDurationPanelPause).SetUpdate(true); // regulates the fade
        await pausePanelRect.DOAnchorPosY(_topPosPanelPauseY, _tweenDurationPanelPause).SetUpdate(true).AsyncWaitForCompletion(); // makes the animation asynchronous so it waits for the animation to end
        await pauseButtonRect.DOAnchorPosY(_topPosButtonPauseX, _tweenDurationButtonPause).SetUpdate(true).AsyncWaitForCompletion();
    }
}
