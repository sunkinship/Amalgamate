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
    public static GameObject currentMovable;
    public static bool isMovingObject;
    public static bool canDropObject;
    static bool canPickUp;
    public GameObject prompt;

    private static Collider2D heldItemCollider;

    [SerializeField] private LayerMask wallLayerMask;

    private static Renderer heldObjectRender;
    private static Renderer playerRender;

    private static Vector3 direction;

    public Animator ani;

    [SerializeField] private AudioClip placeBlock;
    [SerializeField] private float volume;

    private void Awake()
    {
        playerRender = player.GetComponent<Renderer>();
        playerInput = player.GetComponent<PlayerInput>();
        canPickUp = true;
    }


    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) || isMovingObject == true)
        //{
        //    StopCoroutine(ButtonPrompt());
        //    prompt.SetActive(false);
        //}

        //Pick up
        if (playerInput.actions["Interact"].triggered && isFacingMovable == true && isMovingObject == false && canPickUp == true && playerMovement.inDialogue == false)
        {
            playerMovement.carryingObject = true;
            prompt.SetActive(false);
            ani.SetBool("isCarrying", true);
            canPickUp = false;
            heldObjectRender = heldItem.GetComponent<Renderer>();
            heldObjectRender.GetComponent<PositionRendering>().enabled = false;
            heldItem.GetComponent<BoxCollider2D>().enabled = false;
            heldItemCollider = heldItem.GetComponent<BoxCollider2D>();
            //Debug.Log("initial false");
            canDropObject = false;
            isMovingObject = true;
            Invoke("EnableDrop", 0.2f);
        }
        //Drop
        if (playerInput.actions["Interact"].triggered && isMovingObject == true && canDropObject == true)
        {
            playerMovement.carryingObject = false;
            AudioManager.Instance.PlaySound(placeBlock, 0.5f);
            heldItem.GetComponentInChildren<ParticleSystem>().Play();
            ani.SetBool("isCarrying", false);
            canPickUp = false;
            heldObjectRender.GetComponent<PositionRendering>().enabled = true;
            heldItem.GetComponent<BoxCollider2D>().enabled = true;
            isMovingObject = false;
            currentMovable.transform.position = this.gameObject.transform.position;
            currentMovable = null;
            heldItem = null;
            canDropObject = false;
            Invoke("CanPickUp", 0.2f);
        }

        if (isMovingObject == true)
        {
            heldObjectRender.sortingOrder = playerRender.sortingOrder + 100;
            currentMovable.transform.position = player.transform.position + currentMovable.GetComponent<holdableObject>().holdLocation;

            //if (playerMovement.inDialogue == true)
            //{
            //    canDropObject = false;
            //}

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

    private void EnableDrop()
    {
        canDropObject = true;
    }

    private void CanPickUp()
    {
        canPickUp = true;
    }


    //public IEnumerator ButtonPrompt()
    //{
    //    Debug.Log("set active");
    //    yield return new WaitForSeconds(2);
    //    prompt.SetActive(true);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject" && isMovingObject == false)
        {
            isFacingMovable = true;
            heldItem = collision.gameObject;
            currentMovable = collision.gameObject;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "moveableObject" && playerMovement.carryingObject == false)
        {
            //StartCoroutine(ButtonPrompt());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject")
        {
            isFacingMovable = false;
            //StopCoroutine(ButtonPrompt());
            //prompt.SetActive(false);
        }
    }
}
