using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{

    public bool removeWall;
    public bool bigPlate;
    public bool triggered;

    public Collider2D wallToRemove;

    public GameObject player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(bigPlate == true)
        {
            if (collision.gameObject.tag == "movableObject" && collision.gameObject.name.Contains("BIG") && player.GetComponent<playerMovement>().carryingObject == false)
            {
                triggered = true;
                if(removeWall == true && wallToRemove != null)
                {
                    wallToRemove.enabled = false;
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
                    wallToRemove.enabled = false;
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
                wallToRemove.enabled = true;
            }
        }
    }

}
