using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool hasQuest; 
    public bool isActive;
    public bool isComplete;
    public bool isPostQuest;
    public string name;
    public string npc;
    public string description;

    public QuestGoal goal;
}
