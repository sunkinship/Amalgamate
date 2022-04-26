using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCWalk : MonoBehaviour
{
    public AILerp aiLerp;
    public Seeker seeker;
    public Patrol patrol;
    public Animator anim;
    public GameObject hotZone;
    public GameObject triggerArea;
    void Start()
    {
        anim = GetComponent<Animator>();
        aiLerp = GetComponent<AILerp>();
        seeker = GetComponent<Seeker>();
        patrol = GetComponent<Patrol>();
    }

    void Update()
    {
        anim.SetBool("isMoving", true);
        if (aiLerp.velocity.x >= 0.01f && aiLerp.canMove == true)
        {
            anim.Play("NPCWalkright");
        }
        else if (aiLerp.velocity.x <= -0.01f && aiLerp.canMove == true)
        {
            anim.Play("NPCWalkleft");
        }
        else if (aiLerp.velocity.y >= 0.01f && aiLerp.canMove == true)
        {
            anim.Play("NPCWalkUp");
        }
        else if (aiLerp.velocity.y <= -0.01f && aiLerp.canMove == true)
        {
            anim.Play("NPCWalkDown");
        }
        else if(aiLerp.reachedEndOfPath)
        {
            anim.SetBool("isMoving", false);
            StartCoroutine(PatrolWait());
        }

        if (aiLerp.canMove == false)
        {
            anim.SetBool("isMoving", false);
        }
    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(patrol.delay);
        anim.SetBool("isMoving", true);
        if(aiLerp.reachedEndOfPath == false && aiLerp.canMove == false)
        {
            StopCoroutine(PatrolWait());
            anim.SetBool("isMoving", false);
        }
    }


}
