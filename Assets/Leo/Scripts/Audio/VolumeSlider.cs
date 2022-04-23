﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeMasterVolume(val));
    }
}
