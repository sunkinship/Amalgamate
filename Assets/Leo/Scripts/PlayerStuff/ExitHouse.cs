using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHouse : MonoBehaviour
{
    public Animator anim;
    public string sceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fade());
        }


    }

    public IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
