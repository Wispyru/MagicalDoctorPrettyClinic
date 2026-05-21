using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class WinLoseCondition : MonoBehaviour
{
    private float _waitForScreenSec = 3f;
    private VideoPlayer _animationPlayer;

    [SerializeField]
    private GameObject _videoPlayerHolder;

    private void Start()
    {
        _animationPlayer = _videoPlayerHolder.GetComponent<VideoPlayer>();
    }

    public void CheckForCurrentTime()
    {
        if (GameData.CurrentTimeInSeconds > 0) return;

        CheckForPoints();


    }

    /// <summary>
    /// Checks the points that you have garnered at the end of the level when the time runs out.
    /// </summary>
    public void CheckForPoints()
    {
        if (GameData.CurrentPoints < GameData.CurrentLevel.RequiredPoints)
        {
            StartCoroutine(EndLevelAnimations(GameData.CurrentLoseAnimation));
        }

        if (GameData.CurrentPoints >= GameData.CurrentLevel.RequiredPoints)
        {
            StartCoroutine(EndLevelAnimations(GameData.CurrentWinAnimation));
        }
    }

    private IEnumerator EndLevelAnimations(VideoClip ClipToPlay)
    {
        //TOPlay animations
        yield return new WaitForSeconds(_waitForScreenSec);
        SceneManager.LoadScene(3);
    }

}
