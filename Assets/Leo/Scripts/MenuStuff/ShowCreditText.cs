using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCreditText : MonoBehaviour
{
    [HideInInspector]
    public GetLights getLights;
    public GameObject trent, leo, logan, tyler, shreyas;

    private void Awake()
    {
        getLights = GameObject.Find("HandleButtonEvents").GetComponent<GetLights>();
    }

    public void TrentGlow()
    {
        trent.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.glowMat;
    }

    public void TrentOff()
    {
        trent.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void LeoGlow()
    {
        leo.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.glowMat;
    }

    public void LeoOff()
    {
        leo.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void LoganGlow()
    {
        logan.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.glowMat;
    }

    public void LoganOff()
    {
        logan.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void TylerGlow()
    {
        tyler.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.glowMat;
    }

    public void TylerOff()
    {
        tyler.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void ShreyasGlow()
    {
        shreyas.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.glowMat;
    }

    public void ShreyasOff()
    {
        shreyas.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }
}
