using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1920, 1200, true);
        //Debug.Log("resolution: " + Screen.currentResolution);
    }
}
