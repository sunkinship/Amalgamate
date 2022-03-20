using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    public QuestGiver vampire;
    public ItemInteractable itemInteractable;
    public GameObject item;

    private bool itemInRange = false;


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (vampire.quest.isActive)
            {
                vampire.AcceptQuest();
                Debug.Log("Quest: [" + vampire.quest.name + "] from [" + vampire.name + "] accepted");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("in range");
            itemInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            Debug.Log("left range");
            itemInRange = false;
        }
    }

    public void PickUpItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemInRange)
            {
                Debug.Log("Picked up item");
                itemInteractable.pickUp();
                inventory.Add(itemInteractable.item);
                item.SetActive(false);
            }
        }
    }

}
