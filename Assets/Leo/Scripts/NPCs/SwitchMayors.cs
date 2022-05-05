﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMayors : MonoBehaviour
{
    public GameObject hiddenMayor;
    public GameObject questMayor;
    public GameObject noQuestMayor;
    private bool mayorActive;

    private void Start()
    {
        if (mayorActive)
        {
            foreach (Quest quest in PlayerManager.quests)
            {
                if (quest.questName.Equals("Find the monster mayor"))
                {
                    SwitchMonsterMayor();
                }
                else if (quest.questName.Equals("Find the human mayor"))
                {
                    SwitchHumanMayor();
                }
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
    }

    /// <summary>
    /// Switches monster mayor to one without a quest if player talks to human mayor first
    /// </summary>
    private void SwitchMonsterMayor()
    {
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
