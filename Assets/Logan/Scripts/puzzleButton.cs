using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class puzzleButton : MonoBehaviour
{
    public GameObject itemToDisable;
    public float time;
    public PlayerInput playerInput;
    public bool canPressButton;



    private void Update()
    {
        if(canPressButton == true && playerInput.actions["Interact"].triggered)
        {
            StartCoroutine(buttonWork());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canPressButton = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPressButton = false;
        }
    }

    public IEnumerator buttonWork()
    {
        itemToDisable.SetActive(false);
        yield return new WaitForSeconds(time);
        itemToDisable.SetActive(true);
    }
}
