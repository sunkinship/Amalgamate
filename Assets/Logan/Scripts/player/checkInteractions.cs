﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class checkInteractions : MonoBehaviour
{
    private bool isKeyDown;
    private bool isFacingInteractable;
    public PlayerInput playerInput;
    public bool isInteractingWithNPC;
  
    private GameObject currentNPC;

    public string nameString;
    public TextMeshProUGUI nameText;
    public GameObject npcPortrait;
    public GameObject forPortrait;

    public GameObject player;
    public ItemGiver npcItem;
    private QuestGiver npcQuest;
    private PlayerManager playerManager;

    //prompt
    public GameObject prompt;

    public bool humanMayor;
    public bool triggerHumanEnd;
    public bool monsterMayor;
    public bool triggerMonsterEnd;

    public GameObject Mayor;

    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        //playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (npcInteract.forcedMayorSpeaking)
        {
            Debug.Log("mayor");
            currentNPC = GameObject.Find("QuestMayor");
        }

        if (playerInput.actions["Interact"].triggered)
        {
            prompt.SetActive(false);
        }

        if(currentNPC != null)
        {
            npcPortrait.GetComponent<Image>().sprite = currentNPC.GetComponent<npcInteract>().currentPortrait;
        }
        
        if(isFacingInteractable == true)
        {
            player.GetComponent<playerMovement>().isFacingNPC = true;
        }
        else
        {
            player.GetComponent<playerMovement>().isFacingNPC = false;
        }

        // Initial check for interaction -> npcInteract -> dialogueManager
        isKeyDown = playerInput.actions["Interact"].triggered;
        // Regular dialogue 
        if (isKeyDown && isFacingInteractable == true && player.GetComponent<playerMovement>().inDialogue == false && player.GetComponent<playerMovement>().speakCooldownLeft <= 0 && player.GetComponent<playerMovement>().carryingObject == false)
        {
            RegularInteraction();
        }
        // Forced dialogue 
        else if (isFacingInteractable == true && currentNPC.GetComponent<npcInteract>().forceDialogue == true && currentNPC.GetComponent<npcInteract>().mustHoldCertainObject == false)
        {
            //Debug.Log("forced collier");
            RegularForecedInteraction();
        }
        // Mayor forced dialogue 
        else if (npcInteract.mayorForcedDialogue == true)
        {
            npcInteract.mayorForcedDialogue = false;
            npcInteract.forcedMayorSpeaking = true;
            currentNPC = Mayor;
            Debug.Log("mayor forced collier");
            //Debug.Log("state: " + Mayor.GetComponent<npcInteract>().mayorForcedDialogue);
            MayorForecedInteraction();
        }
        // Foreced dialogue requiring held object
        else if (isFacingInteractable == true && currentNPC.GetComponent<npcInteract>().forceDialogue == true && currentNPC.GetComponent<npcInteract>().mustHoldCertainObject == true)
        {
            HoldingObjectInteraction();
        }
    }


    /// <summary>
    /// Initiates regular dialogue 
    /// </summary>
    private void RegularInteraction()
    {
        nameString = currentNPC.GetComponent<npcInteract>().NPCName;

        forPortrait.GetComponent<managePortraits>().currentNPC = currentNPC;
        //currentNPC.GetComponent<interactable>()?.Interact(CheckQuestState());
        CheckQuestState();
        nameText.text = nameString;
        player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);
    }


    /// <summary>
    /// Initiates forced dialogue
    /// </summary>
    private void RegularForecedInteraction()
    {
        nameString = currentNPC.GetComponent<npcInteract>().NPCName;

        forPortrait.GetComponent<managePortraits>().currentNPC = currentNPC;
        currentNPC.GetComponent<npcInteract>().UpdateForcedColliders();
        PlayerManager.forcedDialogueEncounters.Add(currentNPC.name);
        CheckQuestState();
        nameText.text = nameString;
        player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);
    }

    /// <summary>
    /// Initiates forced dialogue from mayor
    /// </summary>
    private void MayorForecedInteraction()
    {
        nameString = Mayor.GetComponent<npcInteract>().NPCName;
        //Debug.Log("mayor forced collider");
        //forPortrait.GetComponent<managePortraits>().currentNPC = Mayor;
        //currentNPC.GetComponent<npcInteract>().UpdateForcedColliders();
        PlayerManager.forcedDialogueEncounters.Add(Mayor.name);
        Mayor.GetComponent<interactable>()?.Interact(Mayor.GetComponent<npcInteract>().portraitsNoQuest[0], "None");
        nameText.text = nameString;
        player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);
    }


    /// <summary>
    /// Initiates forced dialogue requiring held object
    /// </summary>
    private void HoldingObjectInteraction()
    {
        if (player.GetComponent<playerMovement>().carryingObject == true)
        {
            nameString = currentNPC.GetComponent<npcInteract>().NPCName;

            forPortrait.GetComponent<managePortraits>().currentNPC = currentNPC;
            //currentNPC.GetComponent<interactable>()?.Interact(CheckQuestState());
            currentNPC.GetComponent<npcInteract>().UpdateForcedColliders();
            PlayerManager.forcedDialogueEncounters.Add(currentNPC.name);
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

        if (npcQuest.quest.hasPreConditionQuest)
        {
            // Check if player has finsished pre condition quest
            npcQuest.CheckforPreCondition();
            PreConditionCheck();
        }
        else if (npcQuest.quest.hasConnectedQuest)
        {
            //Check if player has met pre-condition for connected quest
            npcQuest.CheckforConnectedQuest();
            ConnectedQuestCheck();
        } 
        else
        {
            RegularQuestCheck();
        }
    }

    /// <summary>
    /// Checks quest state for regular quests
    /// </summary>
    private void RegularQuestCheck()
    {
        //Linked quest triggered
        if (npcItem.canGiveItem == true)
        {
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0], "Linked");
        }
        // No available quest
        else if (npcQuest.quest.hasQuest == false)
        {
            //Debug.Log("no quest");
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsNoQuest[0], "None");
        }
        // Regular quest not started
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

    /// <summary>
    /// Checks quest state for connected quests
    /// </summary>
    private void ConnectedQuestCheck()
    {
        //Linked quest triggered
        if (npcItem.canGiveItem == true)
        {
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0], "Linked");
        }
        // Connected quest not received yet
        else if (npcQuest.quest.isConnectedQuest == false)
        {
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsNoQuest[0], "None");
        }
        // Quest not started
        else if (npcQuest.quest.isActive == false && npcQuest.quest.isComplete == false)
        {
            //Debug.Log("quest not started?");
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

    /// <summary>
    /// Checks quest state for pre condition quests
    /// </summary>
    private void PreConditionCheck()
    {
        //Linked quest triggered
        if (npcItem.canGiveItem == true)
        {
            //return currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0];
            currentNPC.GetComponent<interactable>()?.Interact(currentNPC.GetComponent<npcInteract>().portraitsLinkedQuest[0], "Linked");
        }
        // Connected quest not received yet
        else if (npcQuest.quest.isAvailable == false)
        {
            //Debug.Log("not ready:  " + npcQuest.quest.isConnectedQuest);
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

    public IEnumerator buttonPrompt()
    {
        yield return new WaitForSeconds(.26f);
        prompt.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "interactableNPC" && npcInteract.mayorForcedDialogue == false)
        {
            isFacingInteractable = true;

            currentNPC = collision.gameObject;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "interactableNPC" && player.GetComponent<playerMovement>().carryingObject == false && isInteractingWithNPC == false)
        {
            StartCoroutine(buttonPrompt());
        }
        if (other.gameObject.tag == "movableObject" && player.GetComponent<playerMovement>().carryingObject == false)
        {
            StartCoroutine(buttonPrompt());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(buttonPrompt());
        isFacingInteractable = false;
        prompt.SetActive(false);
    }
}
