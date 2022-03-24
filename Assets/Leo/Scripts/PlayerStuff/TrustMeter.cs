using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrustMeter : MonoBehaviour
{
    private Slider slider;

    public float fillSpeed = 10;
    public float targetProgress;
    public float target = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        AddProgress(1f);
    }

    private void Update()
    {
        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;
    }

    public void AddProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
