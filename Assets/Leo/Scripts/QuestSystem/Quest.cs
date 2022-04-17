using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool hasQuest, hasConnectedQuest;
    [HideInInspector]
    public bool isActive, isComplete, isPostQuest, isConnectedQuest;
    public string questName, description, connectedQuestName;

    public QuestGoal goal;
}
