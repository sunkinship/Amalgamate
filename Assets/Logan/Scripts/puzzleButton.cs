using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class puzzleButton : MonoBehaviour
{
    public GameObject itemToDisable;
    public GameObject mirrorItemToDisable;
    public float time;
    public PlayerInput playerInput;
    public bool canPressButton;

    public GameObject cameraToMove;
    private GameObject objectToFocus;
    public GameObject player;
    public GameObject mirrorPlayer;

    public float focusTime;
    public float waitToDisable;

    public float speed = 0.0005f;

    private bool canActivatecanPressButton;

    [SerializeField]
    private bool mirrorScene, monsterScene;

    public GameObject prompt;

    public Sprite pressedSprite;

    public void Start()
    {
        objectToFocus = itemToDisable;
        canActivatecanPressButton = true;
    }

    private void Update()
    {
        if(canPressButton == true && playerInput.actions["Interact"].triggered)
        {
            canPressButton = false;
            StartCoroutine(ButtonWork());
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "interactableNPC") && canActivatecanPressButton == true)
        {
            Debug.Log("entered");
            canPressButton = true;
            prompt.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
            prompt.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "interactableNPC")
        {
            Debug.Log("left");
            canPressButton = false;
            prompt.SetActive(false);
        }
    }

    public IEnumerator ButtonWork()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = pressedSprite;
        playerMovement.inCutScene = true;
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = false;
        prompt.SetActive(false);
        if (monsterScene)
        {
            DestroyDoorToMirror.doorDestroyed = true;
        }
        canActivatecanPressButton = false;
        while(cameraToMove.transform.position != new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10))
        {
            cameraToMove.transform.position = Vector3.MoveTowards(new Vector3(cameraToMove.transform.position.x, cameraToMove.transform.position.y, -10), new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10), speed / Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitToDisable);
        if (mirrorScene)
        {
            mirrorItemToDisable.SetActive(false);
        }
        itemToDisable.SetActive(false);
        yield return new WaitForSeconds(focusTime);
        cameraToMove.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = true;
        if (mirrorScene)
        {
            mirrorPlayer.GetComponent<playerMovement>().enabled = true;
        }
        playerMovement.inCutScene = false;
        //yield return new WaitForSeconds(time);
        //canActivatecanPressButton = true;
        //itemToDisable.SetActive(true);
    }
}
