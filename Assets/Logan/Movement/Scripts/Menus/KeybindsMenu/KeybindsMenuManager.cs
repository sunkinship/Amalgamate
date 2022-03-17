﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeybindsMenuManager : MonoBehaviour
{

    [SerializeField]
    private InputAction pauseButton;
    [SerializeField]
    private Canvas canvas;
    public GameObject settingsCanvas;
    public GameObject mainCanvas;

    private bool paused = false;

    private void OnEnable()
    {
        pauseButton.Enable();
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }

    private void Awake()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
    }

    private void Start()
    {
        pauseButton.performed += _ => Pause();
    }

    public void Pause()
    {
        paused = !paused;
        if(paused)
        {
            Time.timeScale = 0;
            canvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            canvas.enabled = false;
            settingsCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
    }

}
