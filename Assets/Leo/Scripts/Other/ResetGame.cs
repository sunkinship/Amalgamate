using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void Reset()
    {
        PlayerManager.quests.Clear();
        PlayerManager.inventory.Clear();
        PlayerManager.forcedDialogueEncounters.Clear();
        PlayerManager.spawnPoint = new Vector2(0, 0);
    }
}
