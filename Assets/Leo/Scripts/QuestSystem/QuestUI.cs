using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI listText;
    public PlayerManager player;
    private string textContents;
 

    /// <summary>
    /// Updates contents of the quest list UI
    /// </summary>
    public void UpdateList()
    {
        //Debug.Log("updating");
        textContents = "";
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.isActive && quest.isComplete == false && quest.questName != "" && quest.description != "")
            {
                textContents += " *" + quest.questName + "\n -" + quest.description + "\n\n";
            }
        }
        listText.text = textContents;
    }
}
