using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private bool master, music, sfx;

    private void Start()
    {
        if (master)
        {
            AudioManager.Instance.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
        }
        else if (music)
        {
            AudioManager.Instance.ChangeMusicVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMusicVolume(val));
        }
        else if (sfx) 
        {
            AudioManager.Instance.ChangeSFXVolume(slider.value);
            slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeSFXVolume(val));
        }
    }
}
