using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMirrorButton : MonoBehaviour
{
    public GameObject realButton;
    public GameObject fakeButton;

    public static bool buttonEnabled;

    private void Start()
    {
        CheckForButton();
    }

    private void CheckForButton()
    {
        if (buttonEnabled == false)
        {
            foreach (Quest quest in PlayerManager.quests)
            {
                if (quest.questName.Equals("Mirror O' Mirror"))
                {
                    buttonEnabled = true;
                    realButton.SetActive(true);
                    fakeButton.SetActive(false);
                }
            }
        }
    }
}
