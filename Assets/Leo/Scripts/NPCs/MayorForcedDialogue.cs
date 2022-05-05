using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayorForcedDialogue : MonoBehaviour
{
    public npcInteract mayor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            npcInteract.mayorForcedDialogue = true;
            npcInteract.forcedMayorSpeaking = true;
        }
    }
}
