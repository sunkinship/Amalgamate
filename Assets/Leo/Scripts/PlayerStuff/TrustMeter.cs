using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrustMeter : MonoBehaviour
{
    private Slider slider;
    private Animator ani;

    public float fillSpeed;
    private float targetProgress;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        ani = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log("Current value: " + slider.value + " Target value: " + targetProgress);
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("AddProgress", 0.5f);
        if (slider.value <= targetProgress)
            slider.value += fillSpeed * Time.deltaTime;
    }

    public IEnumerator AddProgress(float newProgress)
    {
        ani.Play("TrustMeterDown");
        yield return new WaitForSeconds(1f);

        targetProgress = slider.value + newProgress - 0.005f;
        while (!(slider.value >= targetProgress))
            yield return null;

        yield return new WaitForSeconds(1.5f);
        ani.Play("TrustMeterUp");
    }
}
