using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalQuest : MonoBehaviour
{
    private int questCounter = 0;


    public void CheckForEnding()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.isHuman && quest.isComplete || quest.isHuman == false && quest.isComplete)
            {
                questCounter++;
            }
        }

        if (questCounter >= 2)
        {
            TriggerLastQuest();
        }
    }

    private void TriggerLastQuest()
    {

    }
}
