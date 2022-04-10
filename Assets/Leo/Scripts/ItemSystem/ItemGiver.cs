using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    public Item item;
    public string linkedQuestName;
    public PlayerManager player;

    //[HideInInspector]
    public bool canGiveItem, gaveItem;


    /// <summary>
    /// Gives required item to player if player has received linked quest 
    /// </summary>
    public void CheckToGiveItem()
    {
        foreach (Quest quest in player.quests)
        {
            if (quest.questName.Equals(linkedQuestName) && gaveItem == false)
            {
                canGiveItem = true;
                PickUp();
            }
        }
    }


    /// <summary>
    /// Receive item from npc and add to inventory 
    /// </summary>
    public void PickUp()
    {
        foreach (Item item in player.inventory)
        {
            if (this.item.itemType == item.itemType)
            {
                //Debug.Log("Picked up item type: " + this.item.itemType + " same as " + item.itemType);
                item.quantity++;
                return;
            }
        }
        //Debug.Log("Picked up new item");
        item.pickedUp = true;
        item.quantity = 1;
        player.inventory.Add(item);
    }
}
