using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool hasQuest; 
    [HideInInspector]
    public bool isActive, isComplete, isPostQuest;
    public string questName;
    public string description;

    public QuestGoal goal;
}
