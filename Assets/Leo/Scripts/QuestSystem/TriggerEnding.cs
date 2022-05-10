using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnding : MonoBehaviour
{
    public GameObject player;
    public Vector2 spawnLocation;
    public string sceneToLoad;
    public Animator fadeAni;
    public Animator cameraAni;
    public Animator photoAni;
    private bool calledCoroutine;
    public static bool exitingScene;

    public GameObject endPanel;
    public GameObject photo;

    public bool cameraFlash;

    public Animator meterAni;

    public Canvas fadeCanvas;
    public Canvas endCanvas;

    public AudioClip clip;

    public void GoToEndScene()
    {
        //Debug.Log("GO");
        if (cameraFlash == false)
        {
            //Debug.Log("not working");
            exitingScene = true;
            playerMovement.inLoadingZone = true;
            if (calledCoroutine == false)
            {
                calledCoroutine = true;
                StartCoroutine(FadeAndSpawn());
            }
        }
        else
        {
            //Debug.Log("working");
            StartCoroutine(CamFlash());
        }
    }

    public void MainMenuFromEnd()
    {
        StartCoroutine(GoToMainMenu());
    }

    public IEnumerator FadeAndSpawn()
    {
        fadeAni.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
        calledCoroutine = false;
        playerMovement.inLoadingZone = false;
        PlayerManager.spawnPoint = spawnLocation;
        exitingScene = false;
    }

    private IEnumerator CamFlash()
    {
        //Debug.Log("why");
        cameraAni.SetTrigger("FadeInTrigger");
        AudioManager.Instance.PlaySound(clip, 1f);
        photo.SetActive(true);
        yield return new WaitForSeconds(1f);
        photoAni.SetTrigger("PhotoZoom");
        yield return new WaitForSeconds(10f);
        endPanel.SetActive(true);
    }

    public IEnumerator GoToMainMenu()
    {
        ChangeCanvasOrder();
        fadeAni.SetTrigger("FadeOutSlow");
        yield return new WaitForSeconds(3.8f);
        SceneManager.LoadScene(sceneToLoad);
        ResetGame.Reset();
        calledCoroutine = false;
        playerMovement.inLoadingZone = false;
        PlayerManager.spawnPoint = spawnLocation;
        exitingScene = false;
    }

    private IEnumerator TrustMeterRateing()
    {
        meterAni.Play("TrustMeterDown");
        yield return new WaitForSeconds(1f);
    }

    private void ChangeCanvasOrder()
    {
        fadeCanvas.sortingOrder = 3;
        endCanvas.sortingOrder = 2;
    }
}
