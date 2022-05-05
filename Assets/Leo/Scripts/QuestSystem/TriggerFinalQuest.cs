using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalQuest : MonoBehaviour
{
    private static int humanQuestCounter = 0;
    private static int monsterQuestCounter = 0;

    public SwitchMayors switchMayor;

    private bool mayorActivated;
    

    /// <summary>
    /// Checks if player has completed at least one human and monster quest 
    /// If true then the final quest will be triggered spawning in the mayors
    /// </summary>
    public void CheckForEnding()
    {
        if (mayorActivated == false)
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

            //if (humanQuestCounter >= 1 && monsterQuestCounter >= 1)
            //{
            //    mayorActivated = true;
            //    switchMayor.ActivateMayors();
            //}

            if (monsterQuestCounter >= 1)
            {
                mayorActivated = true;
                switchMayor.ActivateMayors();
            }
        }
    }
}
