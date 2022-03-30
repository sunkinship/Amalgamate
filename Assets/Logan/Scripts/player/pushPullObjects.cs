using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class pushPullObjects : MonoBehaviour
{
    private PlayerInput playerInput;
    public GameObject heldItem;
    public GameObject player;
    public bool isPulling;
    private bool isKeyDown;
    private bool isFacingMovable;
    private static GameObject currentMovable;
    private static bool isMovingObject;



    public void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
    }

    public void Update()
    {

        if(playerInput.actions["Interact"].triggered && isFacingMovable == true && isMovingObject == false)
        {
            isMovingObject = true;
        }
        if (isMovingObject == true && playerInput.actions["Interact"].triggered)
        {
            isMovingObject = false;
            currentMovable = null;
        }

        if (isMovingObject == true)
        {
            player.GetComponent<playerMovement>().moveSpeed = 3;
            currentMovable.transform.position = player.transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject")
        {
            isFacingMovable = true;

            currentMovable = collision.gameObject;

        }

    }

}
