using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class fastTravel : MonoBehaviour
{
    public Animator anim;
    public string sceneToLoad;
    public void OnClickGoHome()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);

    }

}
