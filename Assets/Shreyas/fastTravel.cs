using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class fastTravel : MonoBehaviour
{
    public string sceneName;
    public Animator anim;
    public void OnClickGoHome()
    {
        StartCoroutine(Fade());
    }


    IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }
    //get back to work shreyas
}
