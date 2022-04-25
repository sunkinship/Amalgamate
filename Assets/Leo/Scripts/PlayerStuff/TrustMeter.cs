using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrustMeter : MonoBehaviour
{
    private Slider slider;
    private Animator ani;

    public float fillSpeed;
    private static float targetProgress;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        ani = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log("Current value: " + slider.value + " Target value: " + targetProgress);
        if (slider.value <= targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    StartCoroutine("AddProgress", 0.15f);
        //}
    }

    public IEnumerator AddProgress(float newProgress)
    {
        ani.Play("TrustMeterDown");
        yield return new WaitForSeconds(1f);

        //targetProgress = slider.value + newProgress - 0.005f;
        targetProgress = slider.value + newProgress;
        //while (!(slider.value >= targetProgress))
        //    yield return null;
        while (slider.value < targetProgress){
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        ani.Play("TrustMeterUp");
    }
}
