using System.Collections;
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
    private QuestGiver npc;
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
            currentNPC.GetComponent<interactable>()?.Interact(CheckQuestState());
            nameText.text = nameString;
            player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);

        }
    }

    /// <summary>
    /// Set portrait based on quest state
    /// </summary>
    private Sprite CheckQuestState()
    {
        npc = currentNPC.GetComponent<QuestGiver>();

        // Check current state of quest
        playerManager.ProgressQuest(npc);

        // No available quest
        if (npc.quest.hasQuest == false)
        {
            return currentNPC.GetComponent<npcInteract>().portraitsNoQuest[0];
        }
        // Quest not started
        else if (npc.quest.isActive == false && npc.quest.isComplete == false)
        {
            //Debug.Log("isActive: " + npc.quest.isActive + "isComplete: " + npc.quest.isComplete);
            return currentNPC.GetComponent<npcInteract>().portraitsPreQuest[0];
        }
        // Quest started but not completed
        else if (npc.quest.isActive && npc.quest.isComplete == false)
        {
            //Debug.Log("isActive: " + npc.quest.isActive + "isComplete: " + npc.quest.isComplete);
            return currentNPC.GetComponent<npcInteract>().portraitsMidQuest[0];
        }
        // First interaction after quest completed
        else if (npc.quest.isActive && npc.quest.isComplete && npc.quest.isPostQuest == false)
        {
            //Debug.Log("isActive: " + npc.quest.isActive + "isComplete: " + npc.quest.isComplete);
            return currentNPC.GetComponent<npcInteract>().portraitsPostQuest[0];
        }
        // Post quest complete
        else 
        {
            return currentNPC.GetComponent<npcInteract>().portraitsPostPostQuest[0];
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
