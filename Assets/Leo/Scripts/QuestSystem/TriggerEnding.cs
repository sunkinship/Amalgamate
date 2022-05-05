using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnding : MonoBehaviour
{
    public GameObject EndSceneLoadingZone;

    public void GotoEndScene()
    {
        EndSceneLoadingZone.SetActive(true);
    }
}
