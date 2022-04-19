using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerManager player;

    public QuestUI questUI;

    public bool removesCollider;
    public Collider2D colliderToRemove;

    private void Start()
    {
        CheckToRemoveCollider();
        player.GetComponent<checkInteractions>().UpdateForcedColliders();
    }

    /// <summary>
    /// Receive quest from NPC
    /// </summary>
    public void AcceptQuest()
    {
        PlayerManager.quests.Add(quest);
        PlayerManager.quests[PlayerManager.quests.Count - 1].isActive = true;
        PlayerManager.quests[PlayerManager.quests.Count - 1].isComplete = false;
        questUI.UpdateList();
    }

    /// <summary>
    /// Checks if player has met received connected quest
    /// </summary>
    public void CheckforConnectedQuest()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.isActive && quest.questName.Equals(this.quest.connectedQuestName))
            {
                this.quest.isConnectedQuest = true;
            }
        }
    }

    /// <summary>
    /// Checks if player has met pre-condition quest for quest
    /// </summary>
    public void CheckforPreCondition()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.isComplete && quest.questName.Equals(this.quest.PreQuestName))
            {
                this.quest.isAvailable = true;
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
        CheckToRemoveCollider();
        player.trustMeter.StartCoroutine("AddProgress", 0.5f);
    }

    public void CheckToRemoveCollider()
    {
        if (quest.isPostQuest == true && removesCollider == true)
        {
            colliderToRemove.enabled = false;
        }
    }
}
