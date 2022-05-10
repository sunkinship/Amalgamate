using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vourgnrhgghgggg : MonoBehaviour
{
    public static bool vampQuestSwap;

    public GameObject badVamp;
    public GameObject goodVamp;

    public void Awake()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if(quest.questName.Equals("Alchemical Potion") && quest.isComplete)
            {
                vampQuestSwap = true;
            }
        }
    }

    private void Start()
    {
        if(vampQuestSwap == true)
        {
            badVamp.SetActive(false);
            goodVamp.SetActive(true);
        }
    }
}
