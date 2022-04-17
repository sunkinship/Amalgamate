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

    private static CapsuleCollider2D heldItemCollider;

    [SerializeField] private LayerMask wallLayerMask;

    private static Renderer heldObjectRender;
    private static Renderer playerRender;

    private static Vector3 direction;

    public void Start()
    {
        playerRender = player.GetComponent<Renderer>();
        playerInput = player.GetComponent<PlayerInput>();
        canPickUp = true;
    }

    public void Update()
    {
        //Pick up
        if(playerInput.actions["Interact"].triggered && isFacingMovable == true && isMovingObject == false && canPickUp == true && player.GetComponent<playerMovement>().inDialogue == false)
        {
            canPickUp = false;
            player.GetComponent<playerMovement>().moveSpeed = 2.5f;
            heldObjectRender = heldItem.GetComponent<Renderer>();
            heldObjectRender.GetComponent<PositionRendering>().enabled = false;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = false;
            heldItemCollider = heldItem.GetComponent<CapsuleCollider2D>();
            canDrop = false;
            isMovingObject = true;
            Invoke("enableDrop", .5f);

        }
        //Drop
        if (playerInput.actions["Interact"].triggered && isMovingObject == true && canDrop == true && player.GetComponent<playerMovement>().inDialogue == false && player.GetComponent<playerMovement>().isFacingNPC == false)
        {
            canPickUp = false;
            heldObjectRender.GetComponent<PositionRendering>().enabled = true;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = true;
            isMovingObject = false;
            currentMovable.transform.position = this.gameObject.transform.position;
            player.GetComponent<playerMovement>().carryingObject = false;
            currentMovable = null;
            heldItem = null;
            canDrop = false;
            player.GetComponent<playerMovement>().moveSpeed = 6;
            Invoke("CanPickUp", .5f);
        }

        if(player.GetComponent<playerMovement>().inDialogue == true)
        {
            canDrop = false;
        }

        if (isMovingObject == true)
        {
            player.GetComponent<playerMovement>().carryingObject = true;
            heldObjectRender.sortingOrder = playerRender.sortingOrder + 100;
            currentMovable.transform.position = player.transform.position + currentMovable.GetComponent<holdableObject>().holdLocation;
            Invoke("enableDrop", .5f);

            //if (Physics2D.BoxCast(heldItemCollider.bounds.center, heldItemCollider.bounds.size, 0f, direction, 3f, wallLayerMask))
            //{
            //    Debug.Log("false");
            //    canDrop = false;
            //}
            //else
            //{
            //    Debug.Log("TRUE" + Physics2D.BoxCast(heldItemCollider.bounds.center, heldItemCollider.bounds.size, 0f, direction, 3000f, wallLayerMask));
            //    canDrop = true;
            //}

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
        if (collision.gameObject.tag == "movableObject" && isMovingObject == false)
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
