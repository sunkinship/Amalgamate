using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMayors : MonoBehaviour
{
    public GameObject noQuestMayor;
    public GameObject QuestMayor;

    public void ActivateMayor()
    {
        noQuestMayor.SetActive(false);
        QuestMayor.SetActive(true);
    }
}
