using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteractionZones : MonoBehaviour
{
    public GameObject player;
    public GameObject upTrigger;
    public GameObject downTrigger;
    public GameObject leftTrigger;
    public GameObject rightTrigger;

    private Animator ani;

    private void Awake()
    {
        ani = player.GetComponent<Animator>();
        downTrigger.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetInteractionZone();
    }

    private void SetInteractionZone()
    {
        if (playerMovement.lastFacingDirection.Equals("RIGHT"))
        {
            rightTrigger.SetActive(true);
        }
        else rightTrigger.SetActive(false);

        if (playerMovement.lastFacingDirection.Equals("LEFT"))
        {
            leftTrigger.SetActive(true);
        }
        else leftTrigger.SetActive(false);

        if (playerMovement.lastFacingDirection.Equals("UP"))
        {
            upTrigger.SetActive(true);
        }
        else upTrigger.SetActive(false);

        if (playerMovement.lastFacingDirection.Equals("DOWN"))
        {
            downTrigger.SetActive(true);
        }
        else downTrigger.SetActive(false);
    }
}
