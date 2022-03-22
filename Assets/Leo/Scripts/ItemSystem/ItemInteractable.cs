using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public Item item;

    public PlayerTest player;

    public GameObject itemObject;

    public void pickUp()
    {
        foreach (Item item in player.inventory)
        {
            if (this.item.itemType == item.itemType)
            {
                Debug.Log("Picked up item type: " + this.item.itemType + " same as " + item.itemType);
                item.quantity++;
                Destroy(itemObject);
                return;
            }
        }
        Debug.Log("Picked up new item");
        item.pickedUp = true;
        item.quantity = 1;
        player.inventory.Add(item);
        Destroy(itemObject);
    }
}
