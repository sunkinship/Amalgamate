using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPrompts : MonoBehaviour
{

    public GameObject npcPrompt;
    public GameObject blockPrompt;
    public GameObject buttonPromptt;

    public void Awake()
    {
        npcPrompt = GameObject.Find("npcPrompt");
        blockPrompt = GameObject.Find("objectPrompt");
        buttonPromptt = GameObject.Find("buttonPrompt");
    }
}
