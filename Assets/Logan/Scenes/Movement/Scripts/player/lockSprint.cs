using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockSprint : MonoBehaviour
{

    public GameObject player;
    public GameObject checkmark;


    public void Awake()
    {
        player.GetComponent<playerMovement>().constantSprint = PlayerPrefs.GetInt("lockSprintMove") == 1 ? true : false;
        if (player.GetComponent<playerMovement>().constantSprint == true) checkmark.SetActive(true); else checkmark.SetActive(false);
    }

    public void onCheckBox()
    {
        if(player.GetComponent<playerMovement>().constantSprint == true)
        {
            player.GetComponent<playerMovement>().constantSprint = false;
            PlayerPrefs.SetInt("lockSprintMove", player.GetComponent<playerMovement>().constantSprint ? 1 : 0);
            checkmark.SetActive(false);
        }
        else
        {
            player.GetComponent<playerMovement>().constantSprint = true;
            PlayerPrefs.SetInt("lockSprintMove", player.GetComponent<playerMovement>().constantSprint ? 1 : 0);
            checkmark.SetActive(true);
        }
    }

}
