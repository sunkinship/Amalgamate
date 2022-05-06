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
    private bool calledCoroutine;
    public static bool exitingScene;

    public void GoToEndScene()
    {
        Debug.Log("GO");
        exitingScene = true;
        playerMovement.inLoadingZone = true;
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(FadeAndSpawn());
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
}
