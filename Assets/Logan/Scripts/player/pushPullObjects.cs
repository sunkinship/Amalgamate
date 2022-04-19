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
    public static bool isMovingObject;
    public static bool canDropObject;
    static bool canPickUp;

    private static Collider2D heldItemCollider;

    [SerializeField] private LayerMask wallLayerMask;

    private static Renderer heldObjectRender;
    private static Renderer playerRender;

    private static Vector3 direction;

    public Animator ani;

    private void Awake()
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
            Debug.Log("picked up");
            ani.SetBool("isCarrying", true);
            canPickUp = false;
            player.GetComponent<playerMovement>().moveSpeed = 2.5f;
            heldObjectRender = heldItem.GetComponent<Renderer>();
            heldObjectRender.GetComponent<PositionRendering>().enabled = false;
            heldItem.GetComponent<BoxCollider2D>().enabled = false;
            heldItemCollider = heldItem.GetComponent<BoxCollider2D>();
            canDropObject = false;
            isMovingObject = true;
            player.GetComponent<playerMovement>().carryingObject = true;
            Invoke("enableDrop", 0.2f);

        }
        //Drop
        if (playerInput.actions["Interact"].triggered && isMovingObject == true && canDropObject == true && player.GetComponent<playerMovement>().inDialogue == false && player.GetComponent<playerMovement>().isFacingNPC == false)
        {
            ani.SetBool("isCarrying", false);
            canPickUp = false;
            heldObjectRender.GetComponent<PositionRendering>().enabled = true;
            heldItem.GetComponent<BoxCollider2D>().enabled = true;
            isMovingObject = false;
            currentMovable.transform.position = this.gameObject.transform.position;
            player.GetComponent<playerMovement>().carryingObject = false;
            currentMovable = null;
            heldItem = null;
            canDropObject = false;
            player.GetComponent<playerMovement>().moveSpeed = 6;
            Invoke("CanPickUp", 0.2f);
        }


        if (isMovingObject == true)
        {
            heldObjectRender.sortingOrder = playerRender.sortingOrder + 100;
            currentMovable.transform.position = player.transform.position + currentMovable.GetComponent<holdableObject>().holdLocation;

            if (player.GetComponent<playerMovement>().inDialogue == true)
            {
                canDropObject = false;
            }

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
        canDropObject = true;
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
