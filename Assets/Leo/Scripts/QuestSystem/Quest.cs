using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isComplete;
    public string name;
    public string npc;
    public string description;

    public QuestGoal goal;
}
