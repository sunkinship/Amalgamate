using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class dialogueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField]  GameObject dialogueTextBox;
    public TextMeshProUGUI dialogueText;
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

    //public IEnumerator TypeDialogue(string dialogue)
    //{
    //    isTyping = true;
    //    dialogueText.text = dialogue;

    //    int totalVisibleCharacters = dialogue.ToString().Length;
    //    int counter = 0;

    //    //float timer = 0;
    //    float delayTime = 0.03f;
    //    float nextTime = Time.time + delayTime;


    //    foreach (var letter in dialogue.ToCharArray())
    //    {
    //        //Debug.Log("Char: " + counter);
    //        while (true)
    //        {
    //            // Wait for delay to continue
    //            if (Time.time > nextTime)
    //            {
    //                //timer = 0;
    //                nextTime = Time.time + delayTime;
    //                int visibleCount = counter % (totalVisibleCharacters + 1);
    //                if (visibleCount >= totalVisibleCharacters)
    //                {
    //                    break;
    //                }
    //                dialogueText.maxVisibleCharacters = visibleCount;
    //                counter++;
    //                break;
    //            }
    //            yield return null;

    //            // Increment timer
    //            //timer += Time.deltaTime;

    //            //Check for skip input
    //            if (Input.GetKeyDown(KeyCode.A))
    //            {
    //                Debug.Log("Skip");
    //                dialogueText.maxVisibleCharacters = totalVisibleCharacters;
    //                isTyping = false;
    //                break;
    //            }
    //        }
    //    }
    //    Debug.Log("exited");
    //    isTyping = false;
    //}

    public IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = dialogue;

        int totalVisibleCharacters = dialogue.ToString().Length;
        int counter = 0;

        foreach (var letter in dialogue.ToCharArray())
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            dialogueText.maxVisibleCharacters = visibleCount;
            if (visibleCount >= totalVisibleCharacters)
            {
                isTyping = false;
            }
            counter += 1;


            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }
}
