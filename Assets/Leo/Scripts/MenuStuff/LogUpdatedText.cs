using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUpdatedText : MonoBehaviour
{
    public Animator logUpdatedAni;
    private bool calledCoroutine;

    public void LogUpdateAnimation()
    {
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(PlayUpdateAni());
        }
    }

    private IEnumerator PlayUpdateAni()
    {
        //Debug.Log("one");
        logUpdatedAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(3);
        //Debug.Log("two");
        logUpdatedAni.SetTrigger("TriggerLeft");
        calledCoroutine = false;
    }
}
