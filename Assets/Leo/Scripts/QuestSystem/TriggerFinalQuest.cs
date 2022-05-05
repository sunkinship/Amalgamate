using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinalQuest : MonoBehaviour
{
    public static int humanQuestCounter = 0;
    public static int monsterQuestCounter = 0;

    public SwitchMayors switchMayor;

    private bool mayorActivated;

    private void Start()
    {
        CheckForFinalQuest();
    }


    /// <summary>
    /// Checks if player has completed at least one human and monster quest 
    /// If true then the final quest will be triggered spawning in the mayors
    /// </summary>
    public void CheckForFinalQuest()
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

            if (humanQuestCounter >= 0)
            {
                //foreach (Quest quest in PlayerManager.quests)
                //{
                //    if (quest.questName.Equals("Find the Monster Mayor") || quest.questName.Equals("Find the Human Mayor"))
                //    {
                //        switchMayor.
                //        return;
                //    }
                //}
                mayorActivated = true;
                switchMayor.ActivateMayors();
            }
        }
    }
}
