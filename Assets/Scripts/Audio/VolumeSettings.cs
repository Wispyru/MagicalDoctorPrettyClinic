using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider _musicSlider;

    private const string MusicVolumeKey = "musicVolume";
    private const string MusicMixerParam = "music";

    private void Awake()
    {
        LoadVolume();
    }

    /// <summary>
    /// Called by the music slider OnValueChanged event in the Inspector.
    /// Converts the slider value to decibels and saves it to PlayerPrefs.
    /// </summary>
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        ApplyVolume(volume);
        SaveVolume(volume);
    }

    /// <summary>
    /// Loads the saved volume from PlayerPrefs and applies it to
    /// the slider and mixer. Defaults to 1 if no value is saved yet.
    /// </summary>
    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        if (_musicSlider == null) return;
        _musicSlider.value = savedVolume;
        ApplyVolume(savedVolume);
    }

    /// <summary>
    /// Converts a linear slider value to logarithmic decibels
    /// and applies it to the audio mixer.
    /// </summary>
    private void ApplyVolume(float volume)
    {
        _audioMixer.SetFloat(MusicMixerParam, Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Saves the current volume value to PlayerPrefs.
    /// </summary>
    private void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }
}