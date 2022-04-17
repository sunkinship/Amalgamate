using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerManager player;

    public QuestUI questUI;


    /// <summary>
    /// Receive quest from NPC
    /// </summary>
    public void AcceptQuest()
    {
        Debug.Log("got quest");
        player.quests.Add(quest);
        player.quests[player.quests.Count - 1].isActive = true;
        player.quests[player.quests.Count - 1].isComplete = false;
        questUI.UpdateList();

    }
    /// <summary>
    /// Checks if player has met pre-condition for quest
    /// </summary>
    public void CheckforConnectedQuest()
    {
        Debug.Log("check connected quest");
        foreach (Quest quest in player.quests)
        {
            Debug.Log("list name: " + quest.questName + "  required name: " + this.quest.connectedQuestName);
            if (quest.isActive && quest.questName.Equals(this.quest.connectedQuestName))
            {
                Debug.Log("has connected quest!!!");
                this.quest.isConnectedQuest = true;
            }
        }
    }

    /// <summary>
    /// Makes post quest true and progresses trust meter
    /// </summary>
    /// <param name="collision"></param>
    public void PostQuest()
    {
        quest.isPostQuest = true;
        player.trustMeter.StartCoroutine("AddProgress", 0.5f);
    }
}
