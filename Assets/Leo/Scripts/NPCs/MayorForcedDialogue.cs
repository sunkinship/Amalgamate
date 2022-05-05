using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorForcedDialogue : MonoBehaviour
{
    public npcInteract mayor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && QuestGiver.turnOffForcedMayorCollider == false)
        {
            Debug.Log("turn on mayor collider");
            npcInteract.mayorForcedDialogue = true;
            npcInteract.forcedMayorSpeaking = true;
        }
    }
}
