using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    [HideInInspector]
    public GameObject item;

    void Update()
    {
        PickUpItem();
    }

    public void GetQuest(QuestGiver npc)
    {
        if (!npc.quest.isActive)
        {
            npc.AcceptQuest();
            Debug.Log("Quest: [" + npc.quest.name + "] from [" + npc.name + "] accepted");
        }
    }

    public void ProgressQuest(QuestGiver npc)
    {
        if (npc.quest.isActive)
        {
            foreach (Item item in inventory)
            {
                if (npc.quest.goal.IsReached(item))
                {
                    npc.quest.isComplete = true;
                    npc.quest.isActive = false;
                    Debug.Log("Quest: [" + npc.quest.name + "] from [" + npc.name + "] complete");
                    return;
                }
            }
            Debug.Log("Quest: [" + npc.quest.name + "] from [" + npc.name + "] not complete");
            return;
        }
        Debug.Log("Quest: [" + npc.quest.name + "] from [" + npc.name + "] not started");
        return;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("in range");
            item = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("left range");
            item = null;
        }
    }

    public void PickUpItem()
    {
        if (item != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked up item");
                item.GetComponent<ItemInteractable>().pickUp();
            }
        }
    }

}
