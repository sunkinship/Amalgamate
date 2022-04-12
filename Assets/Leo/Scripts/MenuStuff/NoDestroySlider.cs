using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroySlider : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
