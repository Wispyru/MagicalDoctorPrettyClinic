using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    //[SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20); // "music" is the name of the exposed parameter in the audiomixer    
        PlayerPrefs.SetFloat("musicVolume", volume); // saves the data of the players prefrence in the music slider
    }

    //public void SetSFXVolume()
    //{
    //float volume = SFXSlider.value;
    //mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    //PlayerPrefs.SetFloat("SFXVolume", volume);
    //}

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        //SetSFXVolume();

    }
}
