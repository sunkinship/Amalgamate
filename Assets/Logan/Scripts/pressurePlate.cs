using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pressurePlate : MonoBehaviour
{

    public bool removeWall;
    public bool bigPlate;
    public bool triggered;

    public GameObject wallToRemove;

    public GameObject player;

    //camera follow stuff
    public PlayerInput playerInput;
    public GameObject cameraToMove;
    private GameObject objectToFocus;
    public float focusTime;
    public float waitToDisable;
    public float speed = 0.0005f;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(bigPlate == true)
        {
            if (collision.gameObject.tag == "movableObject" && collision.gameObject.name.Contains("BIG") && player.GetComponent<playerMovement>().carryingObject == false)
            {
                triggered = true;
                if(removeWall == true && wallToRemove != null)
                {
                    StartCoroutine(plateWork());
                }
            }
        }
        else if(bigPlate == false)
        {
            if (collision.gameObject.tag == "movableObject" && player.GetComponent<playerMovement>().carryingObject == false)
            {
                triggered = true;
                if (removeWall == true && wallToRemove != null)
                {
                    StartCoroutine(plateWork());
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "movableObject")
        {
            triggered = false;
            if (removeWall == true && wallToRemove != null)
            {
                wallToRemove.SetActive(true);
            }
        }
    }

    public IEnumerator plateWork()
    {
        objectToFocus = wallToRemove;
        player.GetComponent<playerMovement>().enabled = false;
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = false;
        while (cameraToMove.transform.position != new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10))
        {
            cameraToMove.transform.position = Vector3.MoveTowards(new Vector3(cameraToMove.transform.position.x, cameraToMove.transform.position.y, -10), new Vector3(objectToFocus.transform.position.x, objectToFocus.transform.position.y, -10), speed / Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(waitToDisable);
        wallToRemove.SetActive(false);
        yield return new WaitForSeconds(focusTime);
        cameraToMove.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        cameraToMove.GetComponent<CameraFollowPlayer>().enabled = true;
        player.GetComponent<playerMovement>().enabled = true;
    }

}
