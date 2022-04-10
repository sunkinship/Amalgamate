using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour, interactable
{
    public GameObject player;
    public string NPCName;

    [Header ("Dialogue")]
    [SerializeField] dialogue dialoguePreQuest;
    [SerializeField] dialogue dialogueMidQuest;
    [SerializeField] dialogue dialoguePostQuest;
    [SerializeField] dialogue dialoguePostPostQuest;
    [SerializeField] dialogue dialogueLinkedQuest;
    [SerializeField] dialogue dialogueNoQuest;

    [Header ("Portraits")]
    public Sprite[] portraitsPreQuest;
    public Sprite[] portraitsMidQuest;
    public Sprite[] portraitsPostQuest;
    public Sprite[] portraitsPostPostQuest;
    public Sprite[] portraitsLinkedQuest;
    public Sprite[] portraitsNoQuest;

    [HideInInspector]
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

        //Linked quest triggered
        if (currentPortrait == portraitsLinkedQuest[0])
        {
            //Debug.Log("Linked quest triggered");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueLinkedQuest, portraitsLinkedQuest));
        }
        // No available quest
        else if (currentPortrait == portraitsNoQuest[0])
        {
            Debug.Log("No available quest");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueNoQuest, portraitsNoQuest));
        }
        // Quest not started
        else if (currentPortrait == portraitsPreQuest[0])
        {
            Debug.Log("Quest not started");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePreQuest, portraitsPreQuest));
            playerManager.GetQuest(npc);
        } 
        // Quest started but not completed
        else if (currentPortrait == portraitsMidQuest[0])
        {
            Debug.Log("Quest started but not completed");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueMidQuest, portraitsMidQuest));
        }
        // First interaction after quest completed
        else if (currentPortrait == portraitsPostQuest[0])
        {
            Debug.Log("First interaction after quest completed");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostQuest, portraitsPostQuest));
        }
        // Post quest complete
        else
        {
            Debug.Log("Post quest complete");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostPostQuest, portraitsPostPostQuest));
        }

        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
