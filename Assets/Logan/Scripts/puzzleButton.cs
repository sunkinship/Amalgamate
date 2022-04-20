using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class puzzleButton : MonoBehaviour
{
    public GameObject itemToDisable;
    public float time;
    public PlayerInput playerInput;
    public bool canPressButton;

    public GameObject cameraToMove;
    public GameObject objectToFocus;
    public GameObject player;

    public float slideTime;
    public float waitToDisable;


    private void Update()
    {
        if(canPressButton == true && playerInput.actions["Interact"].triggered)
        {
            StartCoroutine(buttonWork());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canPressButton = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPressButton = false;
        }
    }

    public IEnumerator buttonWork()
    {
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = false;
        //cameraToMove.transform.position = new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10);
        cameraToMove.transform.position += new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10) - cameraToMove.transform.position;
        yield return new WaitForSeconds(waitToDisable);
        itemToDisable.SetActive(false);
        yield return new WaitForSeconds(slideTime);
        cameraToMove.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = true;
        yield return new WaitForSeconds(time);
        itemToDisable.SetActive(true);
    }
}
