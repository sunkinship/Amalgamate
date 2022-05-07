﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMirrorButton : MonoBehaviour
{
    public GameObject mirrorSceneButton;
    public GameObject mirrorDoorNPC;

    private void Start()
    {
        foreach (Quest quest in PlayerManager.quests)
        {
            if (quest.questName.Equals("Mirror O' Mirror"))
            {
                Debug.Log("eneabled button");
                mirrorSceneButton.SetActive(true);
                mirrorDoorNPC.SetActive(false);
            }
        }
    }
}
