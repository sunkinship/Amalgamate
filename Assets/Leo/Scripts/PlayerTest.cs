using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    public QuestGiver vampire;
    public GameObject item;

    void Update()
    {
        GetQuest();
        ProgressQuest();
        PickUpItem();
    }

    public void GetQuest()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!vampire.quest.isActive)
            {
                vampire.AcceptQuest();
                Debug.Log("Quest: [" + vampire.quest.name + "] from [" + vampire.name + "] accepted");
            }         
        }
    }

    public void ProgressQuest()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (vampire.quest.isActive)
            {
                foreach (Item item in inventory)
                {
                    if (vampire.quest.goal.IsReached(item))
                    {
                        vampire.quest.isComplete = true;
                        vampire.quest.isActive = false;
                        Debug.Log("Quest: [" + vampire.quest.name + "] from [" + vampire.name + "] complete");
                        return;
                    }
                }   
                Debug.Log("Quest: [" + vampire.quest.name + "] from [" + vampire.name + "] not complete");
                return;
            }
            Debug.Log("Quest: [" + vampire.quest.name + "] from [" + vampire.name + "] not started");
            return;
        }
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
