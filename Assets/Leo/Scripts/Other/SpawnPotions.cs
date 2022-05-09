using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPotions : MonoBehaviour
{
    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;

    void Start()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals("Magical Potion"))
            {
                potion1.SetActive(true);
                potion2.SetActive(true);
                potion3.SetActive(true);
                CheckForPotions();
                return;
            }
        }
    }

    private void CheckForPotions()
    {
        foreach (Item item in PlayerManager.inventory)
        {
            if (item.itemType == ItemType.Potion)
            {
                if (item.itemName.Equals("Potion1"))
                {
                    potion1.SetActive(false);
                }
                else if (item.itemName.Equals("Potion2"))
                {
                    potion2.SetActive(false);
                }
                else if (item.itemName.Equals("Potion3"))
                {
                    potion3.SetActive(false);
                }
            }
        }
    }
}
