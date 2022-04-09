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
    [SerializeField] dialogue dialoguePostPostQuest;
    [SerializeField] dialogue dialogueNoQuest;
    public string NPCName;

    public Sprite[] portraitsPreQuest;
    public Sprite[] portraitsMidQuest;
    public Sprite[] portraitsPostQuest;
    public Sprite[] portraitsPostPostQuest;
    public Sprite[] portraitsNoQuest;

    public Sprite currentPortrait;

    private PlayerManager playerManager;
    private QuestGiver npc;

    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        npc = gameObject.GetComponent<QuestGiver>();
    }

    /// <summary>
    /// Set and write NPC dialogue and portaits based on quest state
    /// </summary>
    public void Interact(Sprite protrait)
    {
        currentPortrait = protrait;

        // No available quest
        if (currentPortrait == portraitsNoQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueNoQuest, portraitsNoQuest));
        }
        // Quest not started
        else if (currentPortrait == portraitsPreQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePreQuest, portraitsPreQuest));
            playerManager.GetQuest(npc);
        } 
        // Quest started but not completed
        else if (currentPortrait == portraitsMidQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueMidQuest, portraitsMidQuest));
        }
        // First interaction after quest completed
        else if (currentPortrait == portraitsPostQuest[0])
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostQuest, portraitsPostQuest));
        }
        // Post quest complete
        else
        {
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostPostQuest, portraitsPostPostQuest));
        }

        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
