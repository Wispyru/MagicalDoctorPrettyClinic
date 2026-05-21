using UnityEngine;
using UnityEngine.Video;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoPlayer.loopPointReached += OnClipFinished;
        PlayStartingAnimation();
    }

    private void PlayStartingAnimation()
    {
        Play(GameData.CurrentStartingAnimation, looping: false);
    }

    public void PlayAttackAnimation()
    {
        Play(GameData.CurrentAttackAnimation, looping: false);
    }

    private void PlayIdleAnimation()
    {
        Play(GameData.CurrentIdleAnimation, looping: true);
    }

    private void Play(VideoClip clip, bool looping)
    {
        if (clip == null) return;
        _videoPlayer.clip = clip;
        _videoPlayer.isLooping = looping;
        _videoPlayer.Play();
    }

    private void OnClipFinished(VideoPlayer vp)
    {
        // After start or attack, fall back to idle
        if (vp.clip == GameData.CurrentStartingAnimation || vp.clip == GameData.CurrentAttackAnimation)
        {
            PlayIdleAnimation();
        }
    }
}
