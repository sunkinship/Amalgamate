using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUpdatedText : MonoBehaviour
{
    public Animator logAni;
    

    public void LogUpdateAnimation()
    {
        StartCoroutine(PlayUpdateAni());
    }

    public void QuestCompleteAnimation()
    {
        StartCoroutine(PlayCompleteAni());
    }

    private IEnumerator PlayUpdateAni()
    {
        logAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(2);
        logAni.SetTrigger("TriggerLeft");
    }

    private IEnumerator PlayCompleteAni()
    {
        logAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(2);
        logAni.SetTrigger("TriggerLeft");
    }
}
