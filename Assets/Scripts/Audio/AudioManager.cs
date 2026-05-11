using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _backgroundMusic;

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
}