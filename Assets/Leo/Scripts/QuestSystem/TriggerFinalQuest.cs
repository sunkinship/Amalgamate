using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalQuest : MonoBehaviour
{
    private static int humanQuestCounter = 0;
    private static int monsterQuestCounter = 0;

    public SwitchMayors switchMayor;
    

    /// <summary>
    /// Checks if player has completed at least one human and monster quest 
    /// If true then the final quest will be triggered
    /// </summary>
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
            switchMayor.ActivateMayor();
        }
    }
}
