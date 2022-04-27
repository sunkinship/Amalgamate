using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleDirections : MonoBehaviour
{
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public NPCWalk NPCparent;

    private void Start()
    {
        //NPCparent = GetComponent<NPCWalk>();


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(right == true)
            {
                NPCparent.anim.Play("NPCIdleRight");
                Debug.Log("Is Working right");
            }
            
            if(left == true)
            {
                NPCparent.anim.Play("NPCIdleLeft");
                Debug.Log("Is Working left");
            }

            if (up == true)
            {
                NPCparent.anim.Play("NPCIdleUp");
                Debug.Log("Is Working up");
            }

            if (down == true)
            {
                NPCparent.anim.Play("NPCIdleDown");
                Debug.Log("Is Working down");
            }
        }
    }
}
