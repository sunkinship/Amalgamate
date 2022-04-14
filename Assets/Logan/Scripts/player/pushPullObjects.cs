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

    [SerializeField] private LayerMask wallLayerMask;

    private static Renderer heldObjectRender;
    private static Renderer playerRender;

    private Vector3 direction;

    public void Start()
    {
        playerRender = player.GetComponent<Renderer>();
        playerInput = player.GetComponent<PlayerInput>();
        canPickUp = true;
    }

    public void Update()
    {
        //Pick up
        if(playerInput.actions["Interact"].triggered && isFacingMovable == true && isMovingObject == false && canPickUp == true)
        {
            canPickUp = false;
            player.GetComponent<playerMovement>().moveSpeed = 2.5f;
            heldObjectRender = heldItem.GetComponent<Renderer>();
            heldObjectRender.GetComponent<PositionRendering>().enabled = false;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = false;
            canDrop = false;
            isMovingObject = true;
            Invoke("enableDrop", .5f);

        }
        //Drop
        if (playerInput.actions["Interact"].triggered && isMovingObject == true && canDrop == true)
        {
            canPickUp = false;
            heldObjectRender.GetComponent<PositionRendering>().enabled = true;
            heldItem.GetComponent<CapsuleCollider2D>().enabled = true;
            isMovingObject = false;
            currentMovable.transform.position = this.gameObject.transform.position;
            currentMovable = null;
            heldItem = null;
            canDrop = false;
            player.GetComponent<playerMovement>().moveSpeed = 6;
            Invoke("CanPickUp", .5f);
        }

        if (isMovingObject == true)
        {
            heldObjectRender.sortingOrder = playerRender.sortingOrder + 100;
            currentMovable.transform.position = player.transform.position;
        }

        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Prevent Dropping in walls
        RaycastHit2D hit;
        RaycastHit2D raycastHit2d = Physics2D.Raycast(player.transform.position, direction, 5, wallLayerMask);
        if (Physics.Raycast(transform.position, Vector3.down, 12, wallLayerMask, QueryTriggerInteraction.Collide))
        {
            
            Debug.DrawRay(player.transform.position, direction, Color.green);
        }
            

        if (raycastHit2d.collider != null)
        {
            canDrop = false;
        }

        direction = Vector2.zero;
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
