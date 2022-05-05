using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMayors : MonoBehaviour
{
    public GameObject hiddenMayor;
    public GameObject questMayor;
    public GameObject noQuestMayor;
    private static bool mayorActive;

    private void Start()
    {
        if (mayorActive)
        {
            CheckCurrentMayorQuest();
        }
    }

    private void CheckCurrentMayorQuest()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals("Find the Monster Mayor"))
            {
                SwitchMonsterMayor();
            }
            else if (quest.questName.Equals("Find the Human Mayor"))
            {
                SwitchHumanMayor();
            }
        }
    }

    /// <summary>
    /// Spawns mayors of both towns to be in front of their houses
    /// </summary>
    public void ActivateMayors()
    {
        hiddenMayor.SetActive(false);
        questMayor.SetActive(true);
        mayorActive = true;
        CheckCurrentMayorQuest();
    }

    /// <summary>
    /// Switches monster mayor to one without a quest if player talks to human mayor first
    /// </summary>
    private void SwitchMonsterMayor()
    {
        Debug.Log("mayor switch");
        questMayor.SetActive(false);
        noQuestMayor.SetActive(true);
    }

    /// <summary>
    /// Switches human mayor to one without a quest if player talks to monster mayor first
    /// </summary>
    private void SwitchHumanMayor()
    {
        questMayor.SetActive(false);
        noQuestMayor.SetActive(true);
    }
}
