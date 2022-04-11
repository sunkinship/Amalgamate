using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toSettings : MonoBehaviour
{
    //[HideInInspector]
    public GameObject mainMenuCanvas, settingsCanvas, creditsCanvas;

    public void Awake()
    {
        //mainMenuCanvas = GameObject.Find("Pause Canvas");
        //settingsCanvas = GameObject.Find("Settings Canvas");
        //creditsCanvas = GameObject.Find("Pause Canvas");
    }

    public void MainMenu()
    {
        DisableCanvas();
        mainMenuCanvas.SetActive(true);
    }

    public void Settings()
    {
        DisableCanvas();
        settingsCanvas.SetActive(true);
    }

    public void Credits()
    {
        DisableCanvas();
        creditsCanvas.SetActive(true);
    }

    private void DisableCanvas()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }
}
