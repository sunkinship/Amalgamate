using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUpdatedText : MonoBehaviour
{
    public Animator logAni;
    private bool calledCoroutine;

    public void LogUpdateAnimation()
    {
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(PlayUpdateAni());
        }
    }

    public void QuestCompleteAnimation()
    {
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(PlayCompleteAni());
        }
    }

    private IEnumerator PlayUpdateAni()
    {
        logAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(2);
        logAni.SetTrigger("TriggerLeft");
        calledCoroutine = false;
    }

    private IEnumerator PlayCompleteAni()
    {
        logAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(2);
        logAni.SetTrigger("TriggerLeft");
        calledCoroutine = false;
    }
}
