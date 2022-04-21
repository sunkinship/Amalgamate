using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCWalk : MonoBehaviour
{
    public AIPath aiPath;
    public Patrol patrol;
    public Animator anim;
    public GameObject hotZone;
    public GameObject triggerArea;

    public Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isMoving", true);
    }

    void Update()
    {
  

        if (aiPath.reachedDestination)
        {
            anim.SetBool("isMoving", false);
            StartCoroutine(PatrolWait());
        }

    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(patrol.delay);
        anim.SetBool("isMoving", true);
    }


}
