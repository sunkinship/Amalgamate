using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class fastTravel : MonoBehaviour
{
    public Animator anim;
    public void OnClickGoHome()
    {
        anim.SetBool("Fade", true);
        StartCoroutine(Fade());
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.5f);
    }

}
