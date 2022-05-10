using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAudio : MonoBehaviour
{
    public AudioClip highlightButton;
    public AudioClip buttonPress;

    public void HighlightButton()
    {
        AudioManager.Instance.PlaySound(highlightButton, 0.5f);
    }

    public void ClickButton()
    {
        AudioManager.Instance.PlaySound(buttonPress, 0.5f);
    }

}
