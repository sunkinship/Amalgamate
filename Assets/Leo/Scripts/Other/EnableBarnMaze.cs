using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBarnMaze : MonoBehaviour
{
    public static bool barnQuest;
    public GameObject barnDoor;

    void Start()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals("Hay in the Barn"))
            {
                barnDoor.SetActive(true);
                return;
            }
        }
    }

}
