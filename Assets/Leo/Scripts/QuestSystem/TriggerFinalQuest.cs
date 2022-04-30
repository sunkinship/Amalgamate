using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalQuest : MonoBehaviour
{
    private int humanQuestCounter = 0;
    private int monsterQuestCounter = 0;


    public void CheckForEnding()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.isHuman == false && quest.isComplete)
            {
                monsterQuestCounter++;
            }
            else if (quest.isHuman && quest.isComplete)
            {
                humanQuestCounter++;
            }
        }

        if (humanQuestCounter >= 1 && monsterQuestCounter >= 1)
        {
            TriggerLastQuest();
        }
    }

    private void TriggerLastQuest()
    {

    }
}
