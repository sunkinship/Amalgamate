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

    private bool isTyping;

    public GameObject portraitManager;
    private GameObject currentNPC;
    private int currentPortraitNumber;
    private Sprite[] portraitList;

    public QuestUI questUI;


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
        if (playerMove.inDialogue == true && playerInput.actions["Interact"].triggered && isTyping == false)
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
                playerMove.inDialogue = false;
                playerMove.speakCooldownLeft = playerMove.speakCooldown;

                // If quest is completed progresses trust meter and makes isPostQuest true
                if (playerManage.callPostQuest && currentNPC.GetComponent<QuestGiver>().quest.isPostQuest == false && currentNPC.GetComponent<QuestGiver>().quest.isComplete == true)
                {
                    currentNPC.GetComponent<QuestGiver>().PostQuest();
                    questUI.UpdateList();
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
        isTyping = true;
        dialogueText.text = dialogue;
        int totalVisibleCharacters = dialogue.ToString().Length;
        int counter = 0;

        float delayTime = 0.03f;
        float nextTime = Time.time;


        foreach (var letter in dialogue.ToCharArray())
        {
            while (true)
            {
                // Wait for delay to continue
                if (Time.time >= nextTime)
                {
                    nextTime = Time.time + delayTime;
                    int visibleCount = counter % (totalVisibleCharacters + 1);
                    dialogueText.maxVisibleCharacters = visibleCount;
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
                    dialogueText.maxVisibleCharacters = totalVisibleCharacters;
                    // Exits both loops
                    goto exitLoops;
                }
            }
        }

        exitLoops:
        isTyping = false;
    }
}