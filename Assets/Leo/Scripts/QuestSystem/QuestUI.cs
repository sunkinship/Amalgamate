using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI listText;
    public List<Quest> questList;
    private string textContents;
    
    private void Start()
    {
        textContents = "       Quests\n";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            panel.SetActive(true);
        } 
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            panel.SetActive(false);
        }
    }

    /// <summary>
    /// Updates contents of the quest list UI
    /// </summary>
    private void UpdateList()
    {
        foreach (Quest quest in questList)
        {
            textContents += "*" + quest.name + "\n  -" + quest.description + "\n";
            if (quest.isActive && quest.isComplete == false)
            {
                textContents += "*" + quest.name + "\n  -" + quest.description + "\n";
            }
            listText.text = textContents;
        }
    }
}
