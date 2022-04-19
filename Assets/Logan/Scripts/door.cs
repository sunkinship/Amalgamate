using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class door : MonoBehaviour
{
    public string sceneToLoad;
    public bool mazeExit;
    public GameObject player;
    public Animator anim;
    public PlayerInput playerInput;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && playerInput.actions["Interact"].triggered)
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
    }
}
