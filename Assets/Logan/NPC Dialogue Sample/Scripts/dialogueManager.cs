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
    public GameObject player;
    public PlayerInput playerInput;
    bool isTyping;
    public GameObject portraitManager;
    public GameObject currentNPC;
    public int currentPortraitNumber;


    public static dialogueManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    dialogue dialogue;
    int currentLine = 0;

    public void Update()
    {

        currentNPC = portraitManager.GetComponent<managePortraits>().currentNPC;

        if (player.GetComponent<playerMovement>().inDialogue == true && playerInput.actions["Interact"].triggered && isTyping == false)
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                currentPortraitNumber++;
                currentNPC.GetComponent<npcInteract>().currentPortrait = currentNPC.GetComponent<npcInteract>().portraitsPreQuest[currentLine];
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogueBox.SetActive(false);
                currentNPC.GetComponent<npcInteract>().currentPortrait = currentNPC.GetComponent<npcInteract>().portraitsPreQuest[0];
                player.GetComponent<playerMovement>().inDialogue = false;
                player.GetComponent<playerMovement>().speakCooldownLeft = player.GetComponent<playerMovement>().speakCooldown;
            }
        }
    }

    public IEnumerator ShowDialogue(dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    // New TypeDialogue Method With Skip Ability 
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


    // Original TypeDialogue Method 

    //public IEnumerator TypeDialogue(string dialogue)
    //{
    //    isTyping = true;
    //    dialogueText.text = dialogue;

    //    int totalVisibleCharacters = dialogue.ToString().Length;
    //    int counter = 0;

    //    foreach (var letter in dialogue.ToCharArray())
    //    {
    //        int visibleCount = counter % (totalVisibleCharacters + 1);
    //        dialogueText.maxVisibleCharacters = visibleCount;
    //        if (visibleCount >= totalVisibleCharacters)
    //        {
    //            isTyping = false;
    //        }
    //        counter += 1;
    //        yield return new WaitForSeconds(0.03f);
    //    }

    //    isTyping = false;
    //}
}