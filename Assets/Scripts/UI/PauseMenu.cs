using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] RectTransform pauseButtonRect;
    [SerializeField] float topPosPanelPauseY, middlePosPanelPauseY;
    [SerializeField] float topPosButtonPauseX, middlePosButtonPauseX;
    [SerializeField] float tweenDurationPanelPause;
    [SerializeField] float tweenDurationButtonPause;
    [SerializeField] CanvasGroup canvasGroup; // Dark panel canvas group

    public void Pause()
    {
        pauseMenu.SetActive(true); // activate pause menu
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenuScene"); // go to main menu scene
        Time.timeScale = 1;
    }

    public async void Resume()
    {
        await PausePanelOutro(); // pause menu WAITS for this fucntion to end until proceding
        pauseMenu.SetActive(false); // deactivate pause menu
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload active scene
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDurationPanelPause).SetUpdate(true); // regulates the fade
        pausePanelRect.DOAnchorPosY(middlePosPanelPauseY, tweenDurationPanelPause).SetUpdate(true); // specify the target position and determine the time to reach set destination
        pauseButtonRect.DOAnchorPosY(middlePosButtonPauseX, tweenDurationButtonPause).SetUpdate(true);
    }


    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDurationPanelPause).SetUpdate(true); // regulates the fade
        await pausePanelRect.DOAnchorPosY(topPosPanelPauseY, tweenDurationPanelPause).SetUpdate(true).AsyncWaitForCompletion(); // makes the animation asynchronous so it waits for the animation to end
        await pauseButtonRect.DOAnchorPosY(topPosButtonPauseX, tweenDurationButtonPause).SetUpdate(true).AsyncWaitForCompletion();
    }
}
