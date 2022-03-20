using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class checkInteractions : MonoBehaviour
{
    bool isKeyDown;
    bool isFacingInteractable;
    public PlayerInput playerInput;
    public bool isInteractingWithNPC;
    public GameObject player;
    GameObject currentNPC;
    public string nameString;
    public Text nameText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        isKeyDown = playerInput.actions["Interact"].triggered;

        if (isKeyDown && isFacingInteractable == true && player.GetComponent<playerMovement>().inDialogue == false && player.GetComponent<playerMovement>().speakCooldownLeft < 0)
        {
            nameString = currentNPC.GetComponent<npcInteract>().NPCname;
            currentNPC.GetComponent<interactable>()?.Interact();
            nameText.text = nameString;
            player.GetComponent<playerMovement>().rb2.velocity = new Vector2(0, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "interactableNPC")
        {
            isFacingInteractable = true;

            currentNPC = collision.gameObject;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isFacingInteractable = false;
    }
    



}
