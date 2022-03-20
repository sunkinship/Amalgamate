using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public Item item;

    public PlayerTest player;

    public void pickUp()
    {
        item.pickedUp = true;
    }
}
