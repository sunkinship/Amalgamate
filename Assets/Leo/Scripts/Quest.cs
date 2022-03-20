using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public string name;
    public string npc;
    public string description;
    public string reward;

    public QuestGoal goal;
}
