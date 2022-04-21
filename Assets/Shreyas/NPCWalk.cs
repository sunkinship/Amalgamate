using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCWalk : MonoBehaviour
{
    public AIPath aiPath;
    public Seeker seeker;
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
            anim.Play("NPCIdle");
            StartCoroutine(PatrolWait());
        }

        if(aiPath.desiredVelocity.x > aiPath.desiredVelocity.y)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                anim.Play("NPCWalkright");
            }
            if (aiPath.desiredVelocity.x <= -0.01f)
            {
                anim.Play("NPCWalkleft");
            }
        }

        if (aiPath.desiredVelocity.x < aiPath.desiredVelocity.y)
        {
            if (aiPath.desiredVelocity.y >= 0.01f)
            {
                anim.Play("NPCWalkUp");
            }
            if (aiPath.desiredVelocity.y <= -0.01f)
            {
                anim.Play("NPCWalkDown");
            }
        }


    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(patrol.delay);
        anim.SetBool("isMoving", true);
    }


}
