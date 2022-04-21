using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLoadingZone : MonoBehaviour
{
    public GameObject player;
    public Vector3 targetPos;
    public float speed;
    public Animator ani;
    private bool moving;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.GetComponent<playerMovement>().inLoadingZone = true;
            if (moving == false)
            {
                moving = true;
                StartCoroutine(MoveToTarget());
            }
        }

    }

    public IEnumerator MoveToTarget()
    {
        while (player.transform.position != targetPos)
        {
            //Debug.Log("move");
            ani.SetBool("isMoving", true);
            ani.SetBool("Right", true);
            ani.SetBool("isCarrying", false);
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        moving = false;
        player.GetComponent<playerMovement>().inLoadingZone = false;
    }
}
