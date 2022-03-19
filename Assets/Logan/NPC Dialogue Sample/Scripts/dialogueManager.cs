using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class dialogueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;
    public GameObject player;
    public PlayerInput playerInput;
    bool isTyping;


    public static dialogueManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    dialogue dialogue;
    int currentLine = 0;

    public void Update()
    {
        if (player.GetComponent<playerMovement>().inDialogue == true && playerInput.actions["Interact"].triggered && isTyping == false)
        {

            ++currentLine;
            if(currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogueBox.SetActive(false);
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

    public IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
