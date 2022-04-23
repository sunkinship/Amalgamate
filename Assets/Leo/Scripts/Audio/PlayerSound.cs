using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void PlaySound()
    {
        AudioManager.Instance.PlaySound(clip);
    }
}
