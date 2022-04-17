using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool hasQuest, hasConnectedQuest, hasPreConditionQuest;
    [HideInInspector]
    public bool isActive, isComplete, isPostQuest, isConnectedQuest, isAvailable;
    public string questName, description, connectedQuestName, PreQuestName;

    public QuestGoal goal;
}
