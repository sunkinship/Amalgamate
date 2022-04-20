using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCWalk : MonoBehaviour
{
    public AIPath aiPath;
    public Patrol patrol;
    Animator anim;
    public Transform NPCDirection;
    Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isMoving", true);
    }

    void Update()
    {
        if(aiPath.reachedDestination)
        {
            anim.SetBool("isMoving", false);
        }
        else if(!aiPath.reachedDestination)
        {
            anim.SetBool("isMoving", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
