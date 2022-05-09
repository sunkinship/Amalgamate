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

    public Animator ani;
    public float waitTime = 0f;


    private enum CurrentCanvas{
        mainmenu, settings, credits
    }

    public void MainMenu()
    {
        dimPanelAni.SetTrigger("DidmFadeIn");
        DisableCanvas();
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
    }

    public void MainMenuFromCredits()
    {
        //getLights.pointLight.enabled = false;
        //getLights.spriteLight.enabled = false;
        dimPanelAni.SetTrigger("DidmFadeIn");
        DisableCanvas();
        ani.Play("PlayerLeftReturn");
        StartCoroutine(ExitCredits());
    }

    public void Settings()
    {
        dimPanelAni.SetTrigger("DidmFadeOut");
        DisableCanvas();
        currentCanvas = CurrentCanvas.settings;
        settingsCanvas.SetActive(true);
    }

    public void Credits()
    {
        //getLights.pointLight.enabled = false;
        //getLights.spriteLight.enabled = false;
        dimPanelAni.SetTrigger("DidmFadeOut");
        DisableCanvas();
        ani.Play("PlayerLeft");
        StartCoroutine(EnterCredits());
    }

    private IEnumerator EnterCredits()
    {
        yield return new WaitForSeconds(waitTime);
        currentCanvas = CurrentCanvas.credits;
        creditsCanvas.SetActive(true);
        //getLights.pointLight.enabled = true;
        //getLights.spriteLight.enabled = true;
    }

    private IEnumerator ExitCredits()
    {
        yield return new WaitForSeconds(waitTime);
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
        //getLights.pointLight.enabled = true;
        //getLights.spriteLight.enabled = true;
    }

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
