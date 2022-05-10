using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vourgnrhgghgggg : MonoBehaviour
{
    public static bool vampQuestSwap;
    public static bool vampQuestSwap2;

    public GameObject noQuestVamp;
    public GameObject GuardQuestVamp;
    public GameObject QuestVamp;

    public void Start()
    {
        CheckVampState();
        SwapVamp();
    }

    private void SwapVamp()
    {
        if (vampQuestSwap == false && vampQuestSwap2 == false)
        {
            noQuestVamp.SetActive(true);
        }
        else if (vampQuestSwap && vampQuestSwap2 == false)
        {
            GuardQuestVamp.SetActive(true);
        }
        else
        {
            QuestVamp.SetActive(true);
        }
    }

    private void CheckVampState()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            Debug.Log("name: " + quest.questName + " complete: " + quest.isComplete);
            if (quest.questName.Equals("Alchemical Potion") && quest.isComplete == false)
            {
                Debug.Log("gaurds");
                vampQuestSwap = true;
                return;
            }
            else if (quest.questName.Equals("Alchemical Potion") && quest.isComplete)
            {
                Debug.Log("vamp quest");
                vampQuestSwap2 = true;
                return;
            }
            else
            {
                Debug.Log("normal");
            }
        }
    }
}
