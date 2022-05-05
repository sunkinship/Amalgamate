using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour, interactable
{
    public GameObject player;
    public string NPCName;
    public AudioClip voiceClip;

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

    public bool forceDialogue;
    [HideInInspector]
    public static bool mayorForcedDialogue;

    public Collider2D forceDialogueCollider;
    public Collider2D afterForceCollider;

    public bool mustHoldCertainObject;


    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        npc = gameObject.GetComponent<QuestGiver>();
    }


    /// <summary>
    /// Removes foreced dialogue colliders and updates state of forced dialogue
    /// </summary>
    public void UpdateForcedColliders()
    {
        forceDialogue = false;
        forceDialogueCollider.enabled = false;
        afterForceCollider.enabled = true;
        //Debug.Log("forced dialogue activated");
    }


    /// <summary>
    /// Set and write NPC dialogue and portaits based on quest state
    /// </summary>
    public void Interact(Sprite protrait, string questState)
    {
        currentPortrait = protrait;

        //Linked quest triggered
        if (questState.Equals("Linked"))
        {
            //Debug.Log("why");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueLinkedQuest, portraitsLinkedQuest));
        }
        // No available quest
        else if (questState.Equals("None"))
        {
            //Debug.Log("No available quest");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueNoQuest, portraitsNoQuest));
        }
        // Quest not started
        else if (questState.Equals("New"))
        {
            //Debug.Log("New quest");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePreQuest, portraitsPreQuest));
            playerManager.GetQuest(npc);
        } 
        // Quest started but not completed
        else if (questState.Equals("Started"))
        {
            //Debug.Log("Quest started but not completed");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogueMidQuest, portraitsMidQuest));
        }
        // First interaction after quest completed
        else if (questState.Equals("Complete"))
        {
            //Debug.Log("First interaction after quest completed");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostQuest, portraitsPostQuest));
        }
        // Second interaction after quest completion  
        else
        {
            //Debug.Log("Post post quest");
            StartCoroutine(dialogueManager.Instance.ShowDialogue(dialoguePostPostQuest, portraitsPostPostQuest));
        }

        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
