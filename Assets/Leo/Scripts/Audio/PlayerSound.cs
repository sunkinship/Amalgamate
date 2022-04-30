using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(clip, volume);
    }
}
