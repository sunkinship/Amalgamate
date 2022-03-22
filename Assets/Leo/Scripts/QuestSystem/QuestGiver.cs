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
        player.quests[player.quests.Count - 1].isActive = true;
        player.quests[player.quests.Count - 1].isComplete = false;
    }

    public bool CompletedQuest()
    {
        foreach (Item item in player.inventory)
        {
            return quest.goal.IsReached(item);
        }

        return false;
    }
}
