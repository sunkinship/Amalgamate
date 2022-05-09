using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    //public GetLights getLights;
    public GameObject mainMenuCanvas, settingsCanvas, creditsCanvas;
    private CurrentCanvas currentCanvas = CurrentCanvas.mainmenu;
    public Animator dimPanelAni;

    //public Animator ani;
    public float waitTime = 0f;


    private enum CurrentCanvas{
        mainmenu, settings, credits
    }

    public void MainMenu()
    {
        dimPanelAni.SetTrigger("DimFadeIn");
        DisableCanvas();
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
    }

    public void MainMenuFromCredits()
    {
        //getLights.pointLight.enabled = false;
        //getLights.spriteLight.enabled = false;
        dimPanelAni.SetTrigger("DimFadeIn");
        DisableCanvas();
        //ani.Play("PlayerLeftReturn");
        //StartCoroutine(ExitCredits());
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
    }

    public void Settings()
    {
        dimPanelAni.SetTrigger("DimFadeOut");
        DisableCanvas();
        currentCanvas = CurrentCanvas.settings;
        settingsCanvas.SetActive(true);
    }

    public void Credits()
    {
        //getLights.pointLight.enabled = false;
        //getLights.spriteLight.enabled = false;
        dimPanelAni.SetTrigger("DimFadeOut");
        DisableCanvas();
        //ani.Play("PlayerLeft");
        //StartCoroutine(EnterCredits());
        currentCanvas = CurrentCanvas.credits;
        creditsCanvas.SetActive(true);
    }

    //private IEnumerator EnterCredits()
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    currentCanvas = CurrentCanvas.credits;
    //    creditsCanvas.SetActive(true);
    //    //getLights.pointLight.enabled = true;
    //    //getLights.spriteLight.enabled = true;
    //}

    //private IEnumerator ExitCredits()
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    currentCanvas = CurrentCanvas.mainmenu;
    //    mainMenuCanvas.SetActive(true);
    //    //getLights.pointLight.enabled = true;
    //    //getLights.spriteLight.enabled = true;
    //}

    private void DisableCanvas()
    {
        switch (currentCanvas)
        {
            case CurrentCanvas.mainmenu:
                mainMenuCanvas.SetActive(false);
                break;
            case CurrentCanvas.settings:
                settingsCanvas.SetActive(false);
                break;
            case CurrentCanvas.credits:
                creditsCanvas.SetActive(false);
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Player House");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
