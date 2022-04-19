using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideFadeCanvas : MonoBehaviour
{
    public Animator anim;
    public string sceneToLoad;
    public GameObject fadeCanvas;

    private void Start()
    {
        Invoke("Hide", 1.5f);
    }
    public void OnClickGoHome()
    {
        fadeCanvas.SetActive(true);
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        anim.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Hide()
    {
        fadeCanvas.SetActive(false);
    }
}
