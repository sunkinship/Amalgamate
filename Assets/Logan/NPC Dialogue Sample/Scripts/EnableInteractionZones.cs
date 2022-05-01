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
    }

    // Update is called once per frame
    void Update()
    {
        SetInteractionZone();
    }

    private void SetInteractionZone()
    {
        if (ani.GetBool("LastRight") == true)
        {
            rightTrigger.SetActive(true);
        }
        else rightTrigger.SetActive(false);

        if (ani.GetBool("LastLeft") == true)
        {
            leftTrigger.SetActive(true);
        }
        else leftTrigger.SetActive(false);

        if (ani.GetBool("LastUp") == true)
        {
            upTrigger.SetActive(true);
        }
        else upTrigger.SetActive(false);

        if (ani.GetBool("LastDown") == true)
        {
            downTrigger.SetActive(true);
        }
        else downTrigger.SetActive(false);
    }
}
