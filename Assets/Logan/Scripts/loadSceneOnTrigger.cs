using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public bool mazeExit;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

            player = collision.gameObject;

            if (mazeExit == true)
            {
                SceneManager.LoadScene(sceneToLoad);
                collision.gameObject.GetComponent<playerMovement>().goToMazeExit = true;

            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }

    }
}
