using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _lockedSFX; // Universal locked sound

    private void Awake()
    {
        SetupSingleton();
    }

    /// <summary>
    /// Ensures only one AudioManager exists across all scenes.
    /// If one already exists, destroys this duplicate.
    /// DontDestroyOnLoad keeps it alive when loading new scenes.
    /// </summary>
    private void SetupSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PlayMusic();
    }

    /// <summary>
    /// Assigns and plays the background music clip.
    /// </summary>
    private void PlayMusic()
    {
        _musicSource.clip = _backgroundMusic;
        _musicSource.Play();
    }

    /// <summary>
    /// Plays any given SFX clip once through the SFX audio source.
    /// Called by UISound for button click sounds.
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays the universal locked sound effect.
    /// Called by UISound when a locked button is clicked.
    /// </summary>
    public void PlayLockedSFX()
    {
        _sfxSource.PlayOneShot(_lockedSFX);
    }
}