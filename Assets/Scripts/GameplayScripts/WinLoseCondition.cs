using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] private GameObject _videoPlayerHolder;
    private bool _levelEnding;
    private VideoPlayer _animationPlayer;
    private bool _clipFinished;

    private void Start()
    {
        _animationPlayer = _videoPlayerHolder.GetComponent<VideoPlayer>();
        _animationPlayer.loopPointReached += OnClipFinished;
    }

    private void Update()
    {
        if (_levelEnding) return;
        CheckForCurrentTime();
    }

    public void CheckForCurrentTime()
    {
        if (GameData.CurrentTimeInSeconds > 0) return;
        _levelEnding = true;
        CheckForPoints();
    }

    /// <summary>
    /// Checks if the player has run out of moves and ends the level if so.
    /// </summary>
    public void CheckForOutOfMoves()
    {
        if (GameData.CurrentMoves > 0) Debug.Log(GameData.CurrentMoves);

        if(GameData.CurrentMoves == 0 && GameData.CurrentRound == 0) CheckForPoints();
    }

    /// <summary>
    /// Checks the points garnered at end of level and plays the appropriate animation.
    /// </summary>
    public void CheckForPoints()
    {
        Debug.Log("Points: " + GameData.CurrentPoints + " Required: " + GameData.CurrentLevel.RequiredPoints);
        bool isWin = GameData.CurrentPoints >= GameData.CurrentLevel.RequiredPoints;
        VideoClip clip = isWin ? GameData.CurrentWinAnimation : GameData.CurrentLoseAnimation;
        int scene = isWin ? 3 : 4;
        StartCoroutine(EndLevelAnimations(clip, scene));
    }

    private IEnumerator EndLevelAnimations(VideoClip clipToPlay, int sceneIndex)
    {
        _clipFinished = false;

        _animationPlayer.clip = clipToPlay;
        _animationPlayer.isLooping = false;
        _animationPlayer.Play();

        yield return new WaitUntil(() => _clipFinished);

        SceneManager.LoadScene(sceneIndex);
    }

    private void OnClipFinished(VideoPlayer vp)
    {
        _clipFinished = true;
    }
}