using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEnding : MonoBehaviour
{
    public GameObject player;
    public Vector2 spawnLocation;
    public string sceneToLoad;
    [HideInInspector]
    public Animator fadeAni;
    private bool calledCoroutine;
    public static bool exitingScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && EnterThisScene.enteringScene == false)
        {
            //Debug.Log("GO");
            exitingScene = true;
            playerMovement.inLoadingZone = true;
            if (calledCoroutine == false)
            {
                calledCoroutine = true;
                StartCoroutine(FadeAndSpawn());
            }
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
