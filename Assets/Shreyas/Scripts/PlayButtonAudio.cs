using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAudio : MonoBehaviour
{
    public GameObject audioObj;
    public GameObject audioObj2;


    public void HighlightAudio()
    {
        Instantiate(audioObj, transform.position, transform.rotation);
    }

    public void ClickAudio()
    {
        Instantiate(audioObj2, transform.position, transform.rotation);
    }
}
