using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource, sfxSource;

    private bool masterMuted;

    public Slider masterSlider;

    public TextMeshProUGUI masterText, musicText, sfxText;

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
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //private void Start()
    //{
    //    Debug.Log("Scene loaded");
    //    masterSlider = GameObject.Find("Master Slider").GetComponent<Slider>();
    //    masterText = GameObject.Find("MasterText").GetComponent<TextMeshProUGUI>();
    //    musicText = GameObject.Find("MusicText").GetComponent<TextMeshProUGUI>();
    //    sfxText = GameObject.Find("SFXText").GetComponent<TextMeshProUGUI>();
    //}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        WaitToAssign();
    }

    private void WaitToAssign()
    {
        Debug.Log("Scene loaded");
        masterSlider = GameObject.Find("Master Slider").GetComponent<Slider>();
        masterText = GameObject.Find("MasterText").GetComponent<TextMeshProUGUI>();
        musicText = GameObject.Find("MusicText").GetComponent<TextMeshProUGUI>();
        sfxText = GameObject.Find("SFXText").GetComponent<TextMeshProUGUI>();
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
        masterText.text = "Master Volume: " + (int) (AudioListener.volume * 100);
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
        musicText.text = "Music Volume: " + (int) (musicSource.volume * 100);
    }

    public void ChangeSFXVolume(float value)
    {
        sfxSource.volume = value;
        sfxText.text = "SFX Volume: " + (int) (sfxSource.volume * 100);
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
