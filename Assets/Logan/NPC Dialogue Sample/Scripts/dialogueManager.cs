using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class dialogueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogueTextBox;
    public TextMeshProUGUI dialogueText;

    [SerializeField] int lettersPerSecond;

    private playerMovement playerMove;
    private PlayerManager playerManage;
    private PlayerInput playerInput;
    public TriggerFinalQuest finalQuestTrigger;
    public TriggerEnding triggerEnd;

    private bool isTyping;

    public GameObject portraitManager;
    private GameObject currentNPC;
    private int currentPortraitNumber;
    private Sprite[] portraitList;

    public QuestUI questUI;
    private AudioClip voiceClip;

    public static dialogueManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        playerInput = gameObject.GetComponent<PlayerInput>();
        playerMove = gameObject.GetComponent<playerMovement>();
        playerManage = gameObject.GetComponent<PlayerManager>();
    }
    dialogue dialogue;
    int currentLine = 0;

    public void Update()
    {
        currentNPC = portraitManager.GetComponent<managePortraits>().currentNPC;

        // Checking for input during NPC interaction
        if (playerMovement.inDialogue == true && playerInput.actions["Interact"].triggered && isTyping == false)
        {
            // Going to next line of dialogue after input
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                currentPortraitNumber++;
                currentNPC.GetComponent<npcInteract>().currentPortrait = portraitList[currentLine];
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            // Resetting values after interaction ended
            else
            {
                currentLine = 0;
                dialogueBox.SetActive(false);
                currentNPC.GetComponent<npcInteract>().currentPortrait = currentNPC.GetComponent<npcInteract>().portraitsPreQuest[0];
                playerMovement.inDialogue = false;
                playerMovement.speakCooldownLeft = playerMovement.speakCooldown;

                // Check if end game condition is triggered from CheckToEnd()
                if (currentNPC.GetComponent<QuestGiver>().triggerEnding) 
                {
                    Debug.Log("trigger eneding");
                    triggerEnd.GoToEndScene();
                }

                // Turn off trigger that sets mayor portarit correctly for forced dialogue
                if (npcInteract.forcedMayorSpeaking)
                {
                    //Debug.Log("reset trigger");
                    npcInteract.forcedMayorSpeaking = false;
                }

                // If quest is completed progresses trust meter and makes isPostQuest true
                if (playerManage.callPostQuest && currentNPC.GetComponent<QuestGiver>().quest.isPostQuest == false && currentNPC.GetComponent<QuestGiver>().quest.isComplete == true)
                {
                    currentNPC.GetComponent<QuestGiver>().PostQuest();
                    questUI.UpdateList();
                    finalQuestTrigger.CheckForFinalQuest();
                }
                // If linked quest item is recevied reset npc dialogue 
                if (currentNPC.GetComponent<ItemGiver>().canGiveItem)
                {
                    currentNPC.GetComponent<ItemGiver>().canGiveItem = false;
                    currentNPC.GetComponent<ItemGiver>().gaveItem = true;
                }
            }
        }
    }

    /// <summary>
    /// Initializes dialogue when interacting with NPCs
    /// </summary>
    /// <param name="dialogue"></param>
    /// <returns></returns>
    public IEnumerator ShowDialogue(dialogue dialogue, Sprite[] portraitList)
    {
        yield return new WaitForEndOfFrame();

        // Set currnet portrait list based on quest state
        this.portraitList = portraitList;

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    /// <summary>
    /// Writes out dialogue letter by letter with skip ability
    /// </summary>
    /// <param name="dialogue"></param>
    /// <returns></returns>
    public IEnumerator TypeDialogue(string dialogue)
    {
        currentNPC = portraitManager.GetComponent<managePortraits>().currentNPC;
        voiceClip = currentNPC.GetComponent<npcInteract>().voiceClip;
        isTyping = true;
        dialogueText.text = dialogue;
        int totalVisibleCharacters = dialogue.ToString().Length;
        int counter = 0;
        int voiceClipCounter = 3;

        float delayTime = 0.03f;
        float nextTime = Time.time;


        foreach (var letter in dialogue.ToCharArray())
        {
            while (true)
            {
                // Wait for delay to continue
                if (Time.time >= nextTime)
                {
                    voiceClipCounter++;
                    nextTime = Time.time + delayTime;
                    int visibleCount = counter % totalVisibleCharacters;
                    dialogueText.maxVisibleCharacters = visibleCount;
                    if (voiceClipCounter >= 3)
                    {
                        //StopSound();
                        PlaySound();
                        voiceClipCounter = 0;
                    }
                    if (visibleCount >= totalVisibleCharacters)
                    {
                        break;
                    }
                    counter++;
                    break;
                }
                yield return null;

                //Check for skip input
                if (playerInput.actions["Interact"].triggered)
                {
                    // Exits both loops
                    goto exitLoops;
                }
            }
        }

        exitLoops:
        //StopSound();
        dialogueText.maxVisibleCharacters = totalVisibleCharacters;
        isTyping = false;
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(voiceClip, 0.5f);
    }

    public void StopSound()
    {
        AudioManager.Instance.StopSound();
    }
}