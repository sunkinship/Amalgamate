using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCWalk : MonoBehaviour
{
    public AILerp aiPath;
    public Seeker seeker;
    public Patrol patrol;
    public Animator anim;
    public GameObject hotZone;
    public GameObject triggerArea;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isMoving", true);
        if (aiPath.velocity.x >= 0.01f)
        {
            anim.Play("NPCWalkright");

        }
        else if (aiPath.velocity.x <= -0.01f)
        {
            anim.Play("NPCWalkleft");
        }
        else if (aiPath.velocity.y >= 0.01f)
        {
            anim.Play("NPCWalkUp");
        }
        else if (aiPath.velocity.y <= -0.01f)
        {
            anim.Play("NPCWalkDown");
        }
        else
        {
            anim.SetBool("isMoving", false);
            StartCoroutine(PatrolWait());
        }

        if (aiPath.canMove == false)
        {
            anim.SetBool("isMoving", false);
        }

    }

    IEnumerator PatrolWait()
    {
        yield return new WaitForSeconds(patrol.delay);
        anim.SetBool("isMoving", true);
    }


}
