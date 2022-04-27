using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Trigger : MonoBehaviour
{
    public NPCWalk NPCparent;
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
            NPCparent.aiLerp.canMove = false;
            NPCparent.anim.SetBool("isMoving", false);
            NPCparent.hotZone.SetActive(true);
        }

    }
}
