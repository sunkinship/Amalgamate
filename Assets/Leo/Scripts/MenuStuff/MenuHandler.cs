using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject mainMenuCanvas, settingsCanvas, creditsCanvas, rebindCanvas;
    private bool mainmenu = true;
    private bool settings, credits, rebind;

    public void MainMenu()
    {
        DisableCanvas();
        mainmenu = true;
        mainMenuCanvas.SetActive(true);
    }

    public void Settings()
    {
        DisableCanvas();
        settings = true;
        settingsCanvas.SetActive(true);
    }

    public void Rebind()
    {
        DisableCanvas();
        rebind = true;
        rebindCanvas.SetActive(true);
    }

    public void Credits()
    {
        DisableCanvas();
        credits = true;
        creditsCanvas.SetActive(true);
    }

    private void DisableCanvas()
    {
        if (mainmenu)
        {
            mainMenuCanvas.SetActive(false);
            mainmenu = false;
        }
        else if (settings)
        {
            settingsCanvas.SetActive(false);
            settings = false;
        }
        else if (rebind)
        {
            rebindCanvas.SetActive(false);
            rebind = false;
        }
        else if (credits)
        {
            creditsCanvas.SetActive(false);
            credits = false;
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
