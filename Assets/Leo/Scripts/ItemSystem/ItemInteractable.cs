using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteractable : MonoBehaviour
{
    public Item item;

    [HideInInspector]
    public PlayerManager player;

    public PlayerInput playerInput;

    private bool canPickUp;

    public GameObject colliderToDestroy;

    public bool doNotDestroy;

    [SerializeField] private float volume;
    [SerializeField] private AudioClip objectObtained;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        CheckToPickUpItem();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "interactionZone" || collision.tag == "Player")
        {
            //Debug.Log("in range");
            canPickUp = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "interactionZone" || collision.tag == "Player")
        {
            //Debug.Log("left range");
            canPickUp = false;
        }
    }

    public void CheckToPickUpItem()
    {
        if (canPickUp)
        {
            if (playerInput.actions["Interact"].triggered)
            {
                //Debug.Log("Picked up item");
                AudioManager.Instance.PlaySound(objectObtained, 0.5f);
                PickUpItem();
            }
        }
    }

    /// <summary>
    /// Pick up item in range and add to inventory 
    /// </summary>
    public void PickUpItem()
    {
        foreach (Item item in PlayerManager.inventory)
        {
            if (this.item.itemType == item.itemType)
            {
                //Debug.Log("Picked up item type: " + this.item.itemType + " same as " + item.itemType);
                item.quantity++;
                Destroy(gameObject);
                colliderToDestroy.SetActive(false);
                return;
            }
        }
        //Debug.Log("Picked up new item");
        item.pickedUp = true;
        item.quantity = 1;
        PlayerManager.inventory.Add(item);
        if (doNotDestroy)
        {
            colliderToDestroy.SetActive(false);
        }
        Destroy(gameObject);
    }
}