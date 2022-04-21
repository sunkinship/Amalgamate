using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public bool mazeExit;
    public GameObject player;
    public Animator anim;
    public Vector2 spawnLocation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            player = collision.gameObject;

            if (mazeExit == true)
            {
                StartCoroutine(Fade());
                //SceneManager.LoadScene(sceneToLoad);
                collision.gameObject.GetComponent<playerMovement>().goToMazeExit = true;

            }
            else
            {
                //SceneManager.LoadScene(sceneToLoad);
                StartCoroutine(Fade());
            }
        }

    }

    public IEnumerator Fade()
    {
        anim.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
        PlayerManager.spawnPoint = spawnLocation;
    }
}
