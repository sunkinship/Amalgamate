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
    private bool calledCoroutine;
    public static bool exitingScene;

    public GameObject photo;

    public bool cameraFlash;

    public void GoToEndScene()
    {
        Debug.Log("GO");
        if (cameraFlash == false)
        {
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
            StartCoroutine(CamFlash());
        }
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

    public IEnumerator CamFlash()
    {
        cameraAni.SetTrigger("FadeTrigger");
        photo.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
        calledCoroutine = false;
        playerMovement.inLoadingZone = false;
        PlayerManager.spawnPoint = spawnLocation;
        exitingScene = false;
    }
}
