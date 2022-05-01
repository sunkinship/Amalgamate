using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class door : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject player;
    public Animator anim;
    public PlayerInput playerInput;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && playerInput.actions["Interact"].triggered)
        {
            StartCoroutine(Fade());
        }
    }

    public IEnumerator Fade()
    {
        anim.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
