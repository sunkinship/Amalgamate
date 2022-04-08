using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour, interactable
{
    private GameObject player;
    [SerializeField] dialogue dialoguePreQuest;
    [SerializeField] dialogue dialogueMidQuest;
    [SerializeField] dialogue dialoguePostQuest;
    [SerializeField] dialogue dialoguePostPostQuest;
    public string NPCName;
    public Sprite[] portraitsPreQuest;
    public Sprite[] portraitsMidQuest;
    public Sprite[] portraitsPostQuest;
    public Sprite[] portraitsPostPostQuest;
    public Sprite currentPortrait;

    private PlayerManager playerManager;
    private QuestGiver npc;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerManager = player.GetComponent<PlayerManager>();
        npc = gameObject.GetComponent<QuestGiver>();
    }

    /// <summary>
    /// Set and write NPC dialogue and portaits based on quest state
    /// </summary>
    public void Interact(Sprite protrait)
    {
        currentPortrait = protrait;

        // Quest not started
        if (currentPortrait == portraitsPreQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePreQuest, portraitsPreQuest));
            playerManager.GetQuest(npc);
        } 
        // Quest started but not completed
        else if (currentPortrait == portraitsMidQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueMidQuest, portraitsMidQuest));
        }
        // Quest Completed
        else 
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostQuest, portraitsPostQuest));
        }

        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
