using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcInteract : MonoBehaviour, interactable
{
    public GameObject player;
    [SerializeField] dialogue dialogue;
    public string NPCname;
    public Sprite[] portraits;
    public Sprite currentPortrait;
    public Sprite firstPortrait;

    public void Interact()
    {
        currentPortrait = firstPortrait;

        StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogue));
        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
