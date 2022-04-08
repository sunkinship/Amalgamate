using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class pushPullObjects : MonoBehaviour
{
    private PlayerInput playerInput;
    public static GameObject heldItem;
    public GameObject player;
    private bool isKeyDown;
    private bool isFacingMovable;
    private static GameObject currentMovable;
    private static bool isMovingObject;
    static bool canDrop;
    static bool canPickUp;


    public void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        canPickUp = true;
    }

    public void Update()
    {

        if(playerInput.actions["Interact"].IsPressed() && isFacingMovable == true && isMovingObject == false && canPickUp == true)
        {
            player.GetComponent<playerMovement>().moveSpeed = 2.5f;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = false;
            canDrop = false;
            isMovingObject = true;
            Invoke("enableDrop", .5f);
        }

        if (playerInput.actions["Interact"].IsPressed() && isMovingObject == true && canDrop == true)
        {
            canPickUp = false;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = true;
            isMovingObject = false;
            currentMovable = null;
            heldItem = null;
            canDrop = false;
            player.GetComponent<playerMovement>().moveSpeed = 6;
            Invoke("CanPickUp", .5f);
        }

        if (isMovingObject == true)
        {
            currentMovable.transform.position = player.transform.position;
        }

    }

    private void enableDrop()
    {
        canDrop = true;
    }

    private void CanPickUp()
    {
        canPickUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject")
        {
            isFacingMovable = true;
            heldItem = collision.gameObject;
            currentMovable = collision.gameObject;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject")
        {
            isFacingMovable = false;
        }

    }


}
