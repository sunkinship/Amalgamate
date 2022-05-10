using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBarnMaze : MonoBehaviour
{
    public static bool barnQuest;
    public GameObject barnDoor;

    void Update()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals("Hay in the barn"))
            {
                barnDoor.SetActive(true);
                return;
            }
        }
    }

}
