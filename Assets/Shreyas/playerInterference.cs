using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class playerInterference : MonoBehaviour
{
    public AIPath aiPath;
    public Patrol patrol;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            aiPath.canMove = false;
            anim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        aiPath.canMove = true;
        anim.SetBool("isMoving", true);
    }
}
