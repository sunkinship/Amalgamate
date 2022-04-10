﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class checkInteractions : MonoBehaviour
{
    private bool isKeyDown;
    private bool isFacingInteractable;
    public PlayerInput playerInput;
    public bool isInteractingWithNPC;
  
    private GameObject currentNPC;

    public string nameString;
    public Text nameText;
    public GameObject npcPortrait;
    public GameObject forPortrait;

    public GameObject player;
    public ItemGiver npcItem;
    private QuestGiver npcQuest;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        //playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(currentNPC != null)
        {
            npcPortrait.GetComponent<Image>().sprite = currentNPC.GetComponent<npcInteract>().currentPortrait;
        }
        
        //Initial check for interaction -> npcInteract -> dialogueManager
        isKeyDown = playerInput.actions["Interact"].triggered;

        if (isKeyDown && isFacingInteractable == true && player.GetComponent<playerMovement>().inDialogue == false && player.GetComponent<playerMovement>().speakCooldownLeft < 0)
        {
            nameString = currentNPC.GetComponent<npcInteract>().NPCName;

            forPortrait.GetComponent<managePortraits>().currentNPC = currentNPC;
            //currentNPC.GetComponent<interactable>()?.Interact(CheckQuestState());
            CheckQuestState();
            nameText.text = nameString;
            player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);

        }
    }

    /// <summary>
    /// Set portrait based on quest state
    /// </summary>
    private void CheckQuestState()
    {
        npcQuest = currentNPC.GetComponent<QuestGiver>();
        npcItem = currentNPC.GetComponent<ItemGiver>();

        // Check current state of quest
        playerManager.ProgressQuest(npcQuest);

        // Check if player has received linked quest
        npcItem.CheckToGiveItem();

        //Linked quest triggered
        if (npcItem.canGiveItem == true)
        {
            //Debug.Log("wtf: " + npcItem.canGiveItem);
            //return currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0];
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0], "Linked");
        }
        // No available quest
        else if (npcQuest.quest.hasQuest == false)
        {
            //Debug.Log("no quest");
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsNoQuest[0], "None");
        }
        // Quest not started
        else if (npcQuest.quest.isActive == false && npcQuest.quest.isComplete == false)
        {
            //Debug.Log("isActive: " + npcQuest.quest.isActive + ". isComplete: " + npcQuest.quest.isComplete);
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsPreQuest[0], "New");
        }
        // Quest started but not completed
        else if (npcQuest.quest.isActive && npcQuest.quest.isComplete == false)
        {
            //Debug.Log("isActive: " + npcQuest.quest.isActive + ". isComplete: " + npcQuest.quest.isComplete);
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsMidQuest[0], "Started");
        }
        // First interaction after quest completed
        else if (npcQuest.quest.isActive == false && npcQuest.quest.isComplete && npcQuest.quest.isPostQuest == false)
        {
            //Debug.Log("isActive: " + npcQuest.quest.isActive + ". isComplete: " + npcQuest.quest.isComplete + ". isPostQuest " + npcQuest.quest.isPostQuest);
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsPostQuest[0], "Complete");
        }
        // Post quest complete
        else 
        {
            //Debug.Log("isActive: " + npcQuest.quest.isActive + ". isComplete: " + npcQuest.quest.isComplete + ". isPostQuest " + npcQuest.quest.isPostQuest);
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsPostPostQuest[0], "Post");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "interactableNPC")
        {
            isFacingInteractable = true;

            currentNPC = collision.gameObject;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isFacingInteractable = false;
    }
}
