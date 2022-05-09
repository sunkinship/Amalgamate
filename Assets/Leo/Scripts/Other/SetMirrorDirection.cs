using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMirrorDirection : MonoBehaviour
{
    public GameObject player, mirrorPlayer;
    [HideInInspector]
    public Animator playerAni, mirrorAni;
    public string playerDirection;

    private void Awake()
    {
        playerAni = player.GetComponent<Animator>();
        mirrorAni = mirrorPlayer.GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log("setting");
        SetDirection(playerAni);
        SetDirection(mirrorAni);
    }

    private void SetDirection(Animator ani)
    {
        Debug.Log("setting");
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
            case "DownIdle":
                ani.SetBool("isMoving", false);
                ani.SetBool("Down", true);
                ani.SetBool("isCarrying", false);
                break;
            case "UpIdle":
                ani.SetBool("isMoving", false);
                ani.SetBool("Up", true);
                ani.SetBool("isCarrying", false);
                break;
            case "LeftIdle":
                ani.SetBool("isMoving", false);
                ani.SetBool("Left", true);
                ani.SetBool("isCarrying", false);
                break;
            case "RightIdle":
                ani.SetBool("isMoving", false);
                ani.SetBool("Right", true);
                ani.SetBool("isCarrying", false);
                break;
        }
    }
}
