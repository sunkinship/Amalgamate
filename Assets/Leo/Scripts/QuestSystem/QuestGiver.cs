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

    public bool triggerEnding;

    [HideInInspector]
    public static bool turnOffForcedMayorCollider;

    private void Awake()
    {
        //Debug.Log("Checking to turn off forced colliders");

        // Updates quest state after changing scenes
        LoadQuestState();

        // Removes any regular colliders already removed after switching scenes
        CheckToRemoveCollider();

        // If npc name matches name in list, remove forced dialogue collider
        foreach (string npcName in PlayerManager.forcedDialogueEncounters)
        {
            if (npcName.Equals(gameObject.GetComponent<npcInteract>().NPCName))
            {
                if (gameObject.GetComponent<npcInteract>().isMayor)
                {
                    Debug.Log("turn off mayor collider");
                    turnOffForcedMayorCollider = true;
                }
                else
                {
                    gameObject.GetComponent<npcInteract>().UpdateForcedColliders();
                }
            }
        }
    }

    /// <summary>
    /// Receive quest from NPC
    /// </summary>
    public void AcceptQuest()
    {
        //Debug.Log("Accepted Quest: " + quest.questName);
        PlayerManager.quests.Add(quest);
        PlayerManager.quests[PlayerManager.quests.Count - 1].isActive = true;
        PlayerManager.quests[PlayerManager.quests.Count - 1].isComplete = false;
        questUI.UpdateList();
        if (quest.questName != "" && quest.description != "")
        {
            player.logText.LogUpdateAnimation();
        }
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
        //Debug.Log(quest.questName + " complete");
        quest.isPostQuest = true;
        CheckToRemoveCollider();
        player.trustMeter.StartCoroutine("AddProgress", 0.087f);
        player.logText.QuestCompleteAnimation();
    }

    public void CheckToRemoveCollider()
    {
        if (quest.isPostQuest == true && removesCollider == true)
        {
            colliderToRemove.enabled = false;
        }
    }

    /// <summary>
    /// Checks if player aleady has quest and updates quest state in case scene change occured 
    /// </summary>
    private void LoadQuestState()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals(this.quest.questName))
            {
                this.quest = quest;
            }
        }
    }
}
