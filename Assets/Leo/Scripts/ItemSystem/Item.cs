using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public ItemType itemType;

    [HideInInspector]
    public bool pickedUp;
    [HideInInspector]
    public int quantity;
    public string itemName;
    public string description;
}

public enum ItemType
{
    Apple,
    Potion,
    Hay,
    Sapphire
}