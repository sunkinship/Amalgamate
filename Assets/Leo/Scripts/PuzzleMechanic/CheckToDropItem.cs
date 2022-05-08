using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckToDropItem : MonoBehaviour
{
    private bool canDrop = true;

    private void Update()
    {
        if (canDrop)
        {
            pushPullObjects.canDropObject = true;
        }
        else
        {
            Debug.Log("false");
            pushPullObjects.canDropObject = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pushPullObjects.isMovingObject)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                canDrop = false;
                Debug.Log("cannot drop");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pushPullObjects.isMovingObject)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                canDrop = false;
                Debug.Log("cannot drop");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pushPullObjects.isMovingObject)
        {
            if (canDrop == false)
            {
                canDrop = true;
                Debug.Log("can drop");
            }
        }
    }
}
