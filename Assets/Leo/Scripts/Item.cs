using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public ItemType itemType;

    public bool pickedUp;
    public string itemName;
    public string description;
}

public enum ItemType
{
    Apple,
    Potion
}