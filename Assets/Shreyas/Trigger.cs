using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Trigger : MonoBehaviour
{
    private NPCWalk NPCparent;
    // Start is called before the first frame update
    void Start()
    {
        NPCparent = GetComponentInParent<NPCWalk>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            NPCparent.aiPath.canMove = false;
            NPCparent.anim.SetBool("isMoving", false);
            NPCparent.hotZone.SetActive(true);
        }
        if(NPCparent.aiPath.reachedDestination && collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            NPCparent.aiPath.canMove = false;
            NPCparent.anim.SetBool("isMoving", false);
            NPCparent.hotZone.SetActive(true);
        }
    }
}
