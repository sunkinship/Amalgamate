using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public Item item;

    [HideInInspector]
    public PlayerManager player;

    private GameObject itemObject;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        itemObject = this.gameObject;
    }

    /// <summary>
    /// Pick up item in range and add to inventory 
    /// </summary>
    public void PickUp()
    {
        foreach (Item item in PlayerManager.inventory)
        {
            if (this.item.itemType == item.itemType)
            {
                //Debug.Log("Picked up item type: " + this.item.itemType + " same as " + item.itemType);
                item.quantity++;
                Destroy(itemObject);
                return;
            }
        }
        //Debug.Log("Picked up new item");
        item.pickedUp = true;
        item.quantity = 1;
        PlayerManager.inventory.Add(item);
        Destroy(itemObject);
    }
}
