using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitThisScene : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPos;
    public Vector2 spawnLocation;
    public string sceneToLoad;
    public float speed;
    [HideInInspector]
    public Animator playerAni;
    public Animator fadeAni;
    private bool calledCoroutine;
    public string playerDirection;
    public static bool exitingScene;
    [SerializeField]
    private bool exitingHouse;

    private void Awake()
    {
        playerAni = player.GetComponent<Animator>();
    }

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
                StartCoroutine(ExitScene());
                StartCoroutine(FadeAndSpawn());
            }
        }

    }

    public IEnumerator ExitScene()
    {
        while (player.transform.position != targetPos)
        {
            //Debug.Log("move");
            SetDirection();
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator FadeAndSpawn()
    {
        fadeAni.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1f);
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
            case "Left":
                playerAni.SetBool("isMoving", true);
                playerAni.SetBool("Left", true);
                playerAni.SetBool("isCarrying", false);
                break;
            case "Right":
                playerAni.SetBool("isMoving", true);
                playerAni.SetBool("Right", true);
                playerAni.SetBool("isCarrying", false);
                break;
            case "Up":
                playerAni.SetBool("isMoving", true);
                playerAni.SetBool("Up", true);
                playerAni.SetBool("isCarrying", false);
                break;
            case "Down":
                playerAni.SetBool("isMoving", true);
                playerAni.SetBool("Down", true);
                playerAni.SetBool("isCarrying", false);
                break;
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
