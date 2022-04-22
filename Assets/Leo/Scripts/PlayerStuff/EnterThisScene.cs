using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterThisScene : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPos;
    public float speed;
    [HideInInspector]
    public Animator ani;
    public string playerDirection;
    private bool calledCoroutine;
    public static bool enteringScene;

    private void Awake()
    {
        ani = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && ExitThisScene.exitingScene == false)
        {
            Debug.Log("COME");
            enteringScene = true;
            playerMovement.inLoadingZone = true;
            if (calledCoroutine == false)
            {
                calledCoroutine = true;
                StartCoroutine(EnterScene());
            }
        }

    }

    public IEnumerator EnterScene()
    {
        while (player.transform.position != targetPos)
        {
            //Debug.Log("move");
            SetDirection();
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        playerMovement.inLoadingZone = false;
        enteringScene = false;
    }

    private void SetDirection()
    {
        switch (playerDirection)
        {
            case "Left":
                ani.SetBool("isMoving", true);
                ani.SetBool("Left", true);
                ani.SetBool("isCarrying", false);
                break;
            case "Right":
                ani.SetBool("isMoving", true);
                ani.SetBool("Right", true);
                ani.SetBool("isCarrying", false);
                break;
            case "Up":
                ani.SetBool("isMoving", true);
                ani.SetBool("Up", true);
                ani.SetBool("isCarrying", false);
                break;
            case "Down":
                ani.SetBool("isMoving", true);
                ani.SetBool("Down", true);
                ani.SetBool("isCarrying", false);
                break;
        }
    }
}
