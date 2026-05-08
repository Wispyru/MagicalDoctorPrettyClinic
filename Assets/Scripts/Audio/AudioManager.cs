using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip BackGroundMusic;

    private void Start()
    {
        musicSource.clip = BackGroundMusic;
        musicSource.Play();
    }

    //public void PlaySFX(AudioClip clip)
    //{
    //SFXSource.PlayOneShot(clip);
    //}
}
