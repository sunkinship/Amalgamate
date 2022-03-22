using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour, interactable
{
    public GameObject player;
    [SerializeField] dialogue dialoguePreQuest;
    [SerializeField] dialogue dialogueMidQuest;
    [SerializeField] dialogue dialoguePostQuest;
    public string NPCName;
    public Sprite[] portraitsPreQuest;
    public Sprite[] portraitsMidQuest;
    public Sprite[] portraitsPostQuest;
    public Sprite currentPortrait;
    public Sprite firstPortrait;

    public QuestGiver npc;
    public PlayerManager playerManager;

    public void Interact()
    {
        currentPortrait = firstPortrait;

        // Check is quest complete
        playerManager.ProgressQuest();

        // Quest not started
        if (npc.quest.isActive == false && npc.quest.isComplete == false)
        {
            Debug.Log("New quest!!!!");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePreQuest));
            player.GetComponent<playerMovement>().inDialogue = true;
            playerManager.GetQuest();
        } 
        // Quest started but not completed
        else if (npc.quest.isActive && npc.quest.isComplete == false)
        {
            Debug.Log("Doing quest!!!!");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueMidQuest));
            player.GetComponent<playerMovement>().inDialogue = true;
        }
        // Quest Completed
        else 
        {
            Debug.Log("Completed quest!!!!");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostQuest));
            player.GetComponent<playerMovement>().inDialogue = true;
        }
    }
}
