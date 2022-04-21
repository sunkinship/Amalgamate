using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotzone : MonoBehaviour
{
    private NPCWalk NPCparent;
    private bool inRange;
    private void Start()
    {
        NPCparent = GetComponentInParent<NPCWalk>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(NPCparent.patrol.delay);
        inRange = false;
        gameObject.SetActive(false);
        NPCparent.triggerArea.SetActive(true);
        NPCparent.aiPath.canMove = true;
        NPCparent.anim.SetBool("isMoving", true);

    }
}
