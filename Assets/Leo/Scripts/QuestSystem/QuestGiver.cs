using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    [HideInInspector]
    public PlayerManager player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    /// <summary>
    /// Receive quest from NPC
    /// </summary>
    public void AcceptQuest()
    {
        player.quests.Add(quest);
        player.quests[player.quests.Count - 1].isActive = true;
        player.quests[player.quests.Count - 1].isComplete = false;
    }

    /// <summary>
    /// Complete quest if condition has been reached
    /// </summary>
    /// <returns></returns>
    public bool CompletedQuest()
    {
        foreach (Item item in player.inventory)
        {
            return quest.goal.IsReached(item);
        }

        return false;
    }
}
