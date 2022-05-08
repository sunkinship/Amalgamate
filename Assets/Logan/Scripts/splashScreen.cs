using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class splashScreen : MonoBehaviour
{
    public Animator textAnimator;
    public Animator titleAnimator;
    public Animator presentsAnimator;
    public Image splashLogo;
    public Image splashText;
    public Image splashTitle;
    public GameObject donut;

    private void Awake()
    {
        StartCoroutine(splashScreenRoutine());
    }

    public IEnumerator splashScreenRoutine()
    {
        yield return new WaitForSeconds(2);
        donut.SetActive(false);
        presentsAnimator.SetTrigger("startShowLogo");
        yield return new WaitForSeconds(2);
        textAnimator.SetTrigger("startShowText");
        yield return new WaitForSeconds(2);
        presentsAnimator.SetTrigger("startHideLogo");
        textAnimator.SetTrigger("startHideText");
        titleAnimator.SetTrigger("startShowTitle");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("NewMainMenu");
        yield return null;
    }

}
