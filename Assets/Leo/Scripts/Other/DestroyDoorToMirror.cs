using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoorToMirror : MonoBehaviour
{
    public static bool doorDestroyed;

    void Start()
    {
        if (doorDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
