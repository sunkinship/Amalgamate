using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCreditText : MonoBehaviour
{
    //public GetLights getLights;
    public GameObject trent, leo, logan, tyler, shreyas, misc;

    private void SetAllOff()
    {
        trent.SetActive(false);
        leo.SetActive(false);
        logan.SetActive(false);
        tyler.SetActive(false);
        shreyas.SetActive(false);
        misc.SetActive(true);
    }

    public void TrentGlow()
    {
        SetAllOff();
        trent.SetActive(true);
    }

    public void LeoGlow()
    {
        SetAllOff();
        leo.SetActive(true);
    }

    public void LoganGlow()
    {
        SetAllOff();
        logan.SetActive(true);
    }

    public void TylerGlow()
    {
        SetAllOff();
        tyler.SetActive(true);
    }

    public void ShreyasGlow()
    {
        SetAllOff();
        shreyas.SetActive(true);
    }

    public void MiscGlow()
    {
        SetAllOff();
        misc.SetActive(true);
    }
}
