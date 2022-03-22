using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;

    public ItemType requiredType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached(Item item)
    {
        currentAmount = item.quantity;
        if (item.itemType == requiredType)
        {
            return currentAmount >= requiredAmount;
        }
        else
        {
            return false;
        }
    }
}

public enum GoalType
{
    Gather,
    Trade
}
