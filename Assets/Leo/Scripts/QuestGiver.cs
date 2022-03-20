using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerTest player;

    public void AcceptQuest()
    {
        player.quests.Add(quest);
        foreach (Quest quest in player.quests)
        {
            quest.isActive = true;
        }
    }

}
