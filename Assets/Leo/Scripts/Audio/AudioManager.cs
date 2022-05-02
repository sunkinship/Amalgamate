using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource, sfxSource;

    private bool masterMuted;
    public Slider masterSlider;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Play Audio
    public void PlaySound(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }

    public void PlayMusic(AudioClip clip, float volume)
    {
        musicSource.PlayOneShot(clip, volume);
    }
    #endregion


    #region Volume
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        sfxSource.volume = value;
    }
    #endregion


    #region Toggle Mute
    public void ToggleMaster()
    {
        if (masterMuted == false)
        {
            masterMuted = true;
            AudioListener.volume = 0;
        }
        else if (masterMuted)
        {
            masterMuted = false;
            AudioListener.volume = masterSlider.value;
        }
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    #endregion
}
