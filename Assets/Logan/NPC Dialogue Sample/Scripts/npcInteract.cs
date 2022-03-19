using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteract : MonoBehaviour, interactable
{
    public GameObject player;
    [SerializeField] dialogue dialogue;
    public string NPCname;

    public void Interact()
    {
        StartCoroutine(dialogueManager.Instance.ShowDialogue(dialogue));
        player.GetComponent<playerMovement>().inDialogue = true;
    }
}
