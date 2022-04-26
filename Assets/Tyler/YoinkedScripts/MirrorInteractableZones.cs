using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorInteractableZones : MonoBehaviour
{
    public GameObject player;
    public GameObject upTrigger;
    public GameObject downTrigger;
    public GameObject leftTrigger;
    public GameObject rightTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<MirrorPlayer>().lastFacingDirection == ("RIGHT"))
        {
            rightTrigger.SetActive(true);
        }
        else rightTrigger.SetActive(false);

        if (player.GetComponent<MirrorPlayer>().lastFacingDirection == ("LEFT"))
        {
            leftTrigger.SetActive(true);
        }
        else leftTrigger.SetActive(false);

        if (player.GetComponent<MirrorPlayer>().lastFacingDirection == ("UP"))
        {
            upTrigger.SetActive(true);
        }
        else upTrigger.SetActive(false);

        if (player.GetComponent<MirrorPlayer>().lastFacingDirection == ("DOWN"))
        {
            downTrigger.SetActive(true);
        }
        else downTrigger.SetActive(false);
    }
}
