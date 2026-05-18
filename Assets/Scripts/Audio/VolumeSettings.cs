using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private const string MusicVolumeKey = "musicVolume";
    private const string SFXVolumeKey = "sfxVolume";
    private const string MusicMixerParam = "music";
    private const string SFXMixerParam = "sfx";

    private void Start()
    {
        StartCoroutine(LoadVolumeNextFrame());
    }

    /// <summary>
    /// Waits one frame before applying volume so the AudioMixer
    /// is fully initialized and SetFloat calls succeed correctly.
    /// </summary>
    private IEnumerator LoadVolumeNextFrame()
    {
        yield return null;
        LoadVolume();
    }

    /// <summary>
    /// Loads saved volume values from PlayerPrefs and applies them
    /// to both sliders and the mixer. Defaults to 1 if not saved yet.
    /// </summary>
    private void LoadVolume()
    {
        float savedMusic = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float savedSFX = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        if (_musicSlider != null)
        {
            _musicSlider.value = savedMusic;
            ApplyVolume(MusicMixerParam, savedMusic);
        }

        if (_sfxSlider != null)
        {
            _sfxSlider.value = savedSFX;
            ApplyVolume(SFXMixerParam, savedSFX);
        }
    }

    /// <summary>
    /// Called by the music slider OnValueChanged event in the Inspector.
    /// </summary>
    public void SetMusicVolume()
    {
        if (_musicSlider == null) return;
        float volume = _musicSlider.value;
        ApplyVolume(MusicMixerParam, volume);
        SaveVolume(MusicVolumeKey, volume);
    }

    /// <summary>
    /// Called by the SFX slider OnValueChanged event in the Inspector.
    /// </summary>
    public void SetSFXVolume()
    {
        if (_sfxSlider == null) return;
        float volume = _sfxSlider.value;
        ApplyVolume(SFXMixerParam, volume);
        SaveVolume(SFXVolumeKey, volume);
    }

    /// <summary>
    /// Converts a linear slider value to logarithmic decibels
    /// and applies it to the given audio mixer parameter.
    /// </summary>
    private void ApplyVolume(string mixerParam, float volume)
    {
        _audioMixer.SetFloat(mixerParam, Mathf.Log10(volume) * 20);
    }

    /// <summary>
    /// Saves a volume value to PlayerPrefs under the given key.
    /// </summary>
    private void SaveVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }
}