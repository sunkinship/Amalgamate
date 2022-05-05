using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMayors : MonoBehaviour
{
    public GameObject hiddenMayor;
    public GameObject questMayor;
    public GameObject noQuestMayor;
    public static bool mayorActive;
    public bool inHumanTown;
    public bool noMayorInScene;

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
            if (noMayorInScene)
            {
                return;
            }
            else if (quest.questName.Equals("Find the Monster Mayor") && inHumanTown == false)
            {
                SwitchMayor();
                return;
            }
            else if (quest.questName.Equals("Find the Human Mayor") && inHumanTown)
            {
                SwitchMayor();
                return;
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
    /// Switches mayor to one with no quest depending on which one the player talked to first
    /// </summary>
    private void SwitchMayor()
    {
        Debug.Log("mayor switch");
        questMayor.SetActive(false);
        noQuestMayor.SetActive(true);
    }
}
