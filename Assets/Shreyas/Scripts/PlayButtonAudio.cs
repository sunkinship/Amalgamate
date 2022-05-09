using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAudio : MonoBehaviour
{
    public GameObject AudioObj1;
    public GameObject AudioObj2;

    public void HighlightButton()
    {
        Instantiate(AudioObj1, transform.position, transform.rotation);
    }

    public void ClickButton()
    {
        Instantiate(AudioObj2, transform.position, transform.rotation);
    }

}
