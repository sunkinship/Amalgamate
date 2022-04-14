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
        player.quests.Add(quest);
        player.quests[player.quests.Count - 1].isActive = true;
        player.quests[player.quests.Count - 1].isComplete = false;
        questUI.UpdateList();
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
