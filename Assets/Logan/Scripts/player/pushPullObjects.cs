using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class pushPullObjects : MonoBehaviour
{
    private PlayerInput playerInput;
    public GameObject heldItem;
    public GameObject CurrentInteractiveZone;
    public GameObject player;
    public GameObject leftInterZone;
    public GameObject rightInterZone;
    public GameObject downInterZone;
    public GameObject upInterZone;
    public bool isPulling;


    public void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if(player.GetComponent<playerMovement>().lastFacingDirection == "RIGHT" && isPulling == false)
        {
            CurrentInteractiveZone = rightInterZone;
        }
        if (player.GetComponent<playerMovement>().lastFacingDirection == "LEFT" && isPulling == false)
        {
            CurrentInteractiveZone = leftInterZone;
        }
        if (player.GetComponent<playerMovement>().lastFacingDirection == "UP" && isPulling == false)
        {
            CurrentInteractiveZone = upInterZone;
        }
        if (player.GetComponent<playerMovement>().lastFacingDirection == "DOWN" && isPulling == false)
        {
            CurrentInteractiveZone = downInterZone;
        }
    }

}
