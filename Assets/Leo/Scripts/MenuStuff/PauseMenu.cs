using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject pausedCanvas;
    public GameObject settingsCanvas;
    public GameObject questListCanvas;

    public Animator ani;
    public Animator fadeAni;

    private bool calledCoroutine;

    private bool paused = false;

    private void Update()
    {
        if (playerInput.actions["Pause"].triggered)
        {
            Pause();
        }
    }
    public void SettingsCanvas()
    {
        questListCanvas.SetActive(false);
        pausedCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void PasuedCanvas()
    {
        questListCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        pausedCanvas.SetActive(true);
    }

    public void QuestListCanvas()
    {
        settingsCanvas.SetActive(false);
        pausedCanvas.SetActive(false);
        questListCanvas.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("exit");
        Pause();
        if (calledCoroutine == false)
        {
            Debug.Log("calling coroutine");
            calledCoroutine = true;
            StartCoroutine(FadeAni());
        }
    }

    public void ExitFromEnd()
    {
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(FadeAni());
        }
    }

    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pausedCanvas.SetActive(true);
            ani.Play("PausePanelRight");
        }
        else
        {
            Time.timeScale = 1;
            DisableAll();
        }
    }

    private void DisableAll()
    {
        pausedCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        questListCanvas.SetActive(false);
    }

    public IEnumerator FadeAni()
    {
        fadeAni.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("NewMainMenu");
        ResetGame.Reset();
        calledCoroutine = false;
    }
}
