using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySameItem : MonoBehaviour
{
    public GameObject itemToCheck;
    public GameObject colliderToDestroy;

    private void Start()
    {
        ChecToDestroy();
    }

    private void ChecToDestroy()
    {
        foreach (Item item in PlayerManager.inventory)
        {
            if (item.itemName.Equals(itemToCheck.name))
            {
                Destroy(itemToCheck);
                Destroy(colliderToDestroy);
                return;
            }
        }
    }
}
