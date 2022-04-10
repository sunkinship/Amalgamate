using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCreditText : MonoBehaviour
{
    public GetLights getLights;
    public GameObject trent, leo, logan, tyler, shreyas;


    public void TrentGlow()
    {
        trent.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.notButtonMat;
    }

    public void TrentOff()
    {
        trent.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void LeoGlow()
    {
        leo.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.notButtonMat;
    }

    public void LeoOff()
    {
        leo.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void LoganGlow()
    {
        logan.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.notButtonMat;
    }

    public void LoganOff()
    {
        logan.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void TylerGlow()
    {
        tyler.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.notButtonMat;
    }

    public void TylerOff()
    {
        tyler.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }

    public void ShreyasGlow()
    {
        shreyas.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.notButtonMat;
    }

    public void ShreyasOff()
    {
        shreyas.GetComponent<TextMeshProUGUI>().fontSharedMaterial = getLights.hiddenMat;
    }
}
