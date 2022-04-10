using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GetLights getLights;
    public GameObject mainMenuCanvas, settingsCanvas, creditsCanvas, rebindCanvas;
    private CurrentCanvas currentCanvas = CurrentCanvas.mainmenu;

    public Animator ani;
    public float waitTime = 1.5f;


    private enum CurrentCanvas{
        mainmenu, settings, credits, rebind
    }

    public void MainMenu()
    {
        DisableCanvas();
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
    }

    public void MainMenuFromCredits()
    {
        getLights.pointLight.enabled = false;
        getLights.spriteLight.enabled = false;
        DisableCanvas();
        ani.Play("PlayerLeftReturn");
        StartCoroutine(ExitCredits());
    }

    public void Settings()
    {
        DisableCanvas();
        currentCanvas = CurrentCanvas.settings;
        settingsCanvas.SetActive(true);
    }

    public void Rebind()
    {
        DisableCanvas();
        currentCanvas = CurrentCanvas.rebind;
        rebindCanvas.SetActive(true);
    }

    public void Credits()
    {
        getLights.pointLight.enabled = false;
        getLights.spriteLight.enabled = false;
        DisableCanvas();
        ani.Play("PlayerLeft");
        StartCoroutine(EnterCredits());
    }

    private IEnumerator EnterCredits()
    {
        yield return new WaitForSeconds(waitTime);
        currentCanvas = CurrentCanvas.credits;
        creditsCanvas.SetActive(true);
        getLights.pointLight.enabled = true;
        getLights.spriteLight.enabled = true;
    }

    private IEnumerator ExitCredits()
    {
        yield return new WaitForSeconds(waitTime);
        currentCanvas = CurrentCanvas.mainmenu;
        mainMenuCanvas.SetActive(true);
        getLights.pointLight.enabled = true;
        getLights.spriteLight.enabled = true;
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
            case CurrentCanvas.rebind:
                rebindCanvas.SetActive(false);
                break;
            case CurrentCanvas.credits:
                creditsCanvas.SetActive(false);
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
