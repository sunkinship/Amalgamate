using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleteAni : MonoBehaviour
{
    public Animator questCompleteAni;
    private bool calledCoroutine;

    public void QuestCompleteAnimation()
    {
        if (calledCoroutine == false)
        {
            calledCoroutine = true;
            StartCoroutine(PlayCompleteAni());
        }
    }

    private IEnumerator PlayCompleteAni()
    {
        questCompleteAni.SetTrigger("TriggerRight");
        yield return new WaitForSeconds(2);
        questCompleteAni.SetTrigger("TriggerLeft");
        calledCoroutine = false;
    }
}
