using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitThisSceneDOOR : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPos;
    public Vector2 spawnLocation;
    public string sceneToLoad;
    [HideInInspector]
    public Animator playerAni;
    public Animator fadeAni;
    private bool calledCoroutine;
    public string playerDirection;
    public static bool exitingScene;
    bool canLeaveScene;

    public GameObject prompt;

    private void Awake()
    {
        playerAni = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if(canLeaveScene == true && Input.GetKey(KeyCode.E))
        {
            //Debug.Log("GO");
            exitingScene = true;
            playerMovement.inLoadingZone = true;
            if (calledCoroutine == false)
            {
                calledCoroutine = true;
                StartCoroutine(ExitScene());
                StartCoroutine(FadeAndSpawn());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && EnterThisScene.enteringScene == false)
        {
            canLeaveScene = true;
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }

    }

    public IEnumerator ExitScene()
    {
        while (player.transform.position != targetPos)
        {
            prompt.SetActive(false);
            //Debug.Log("move");
            SetDirection();
            player.GetComponent<playerMovement>().enabled = false;
            yield return null;
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

    private void SetDirection()
    {
        switch (playerDirection)
        {

            case "DownIdle":
                playerAni.SetBool("isMoving", false);
                playerAni.SetBool("Down", true);
                playerAni.SetBool("isCarrying", false);
                break;
            case "UpIdle":
                playerAni.SetBool("isMoving", false);
                playerAni.SetBool("Up", true);
                playerAni.SetBool("isCarrying", false);
                break;
        }
    }
}
