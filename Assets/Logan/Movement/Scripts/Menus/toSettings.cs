using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toSettings : MonoBehaviour
{

    public GameObject settingsCanvas;
    public GameObject mainCanvas;

    public void Start()
    {
        //settingsCanvas = GameObject.Find("Settings Canvas");
        //mainCanvas = GameObject.Find("Pause Canvas");
    }

    public void onSettingsClick()
    {
        settingsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }
    public void onBackClick()
    {
        settingsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
