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
 

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Tab))
    //    {
    //        panel.SetActive(true);
    //    } 
    //    if (Input.GetKeyUp(KeyCode.Tab))
    //    {
    //        panel.SetActive(false);
    //    }
    //}

    /// <summary>
    /// Updates contents of the quest list UI
    /// </summary>
    public void UpdateList()
    {
        //Debug.Log("updating");
        textContents = "";
        foreach (Quest quest in PlayerManager.quests)
        {
            //Debug.Log("quest: " + quest.questName);
            if (quest.isActive && quest.isComplete == false)
            {
                textContents = " *" + quest.questName + "\n -" + quest.description + "\n";
            }
        }
        listText.text = textContents;
    }
}
