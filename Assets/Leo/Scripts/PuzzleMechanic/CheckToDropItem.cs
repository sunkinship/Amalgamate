using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckToDropItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pushPullObjects.isMovingObject)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                pushPullObjects.canDropObject = false;
                //Debug.Log("hit");
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pushPullObjects.isMovingObject)
        {
            if (collision.gameObject.tag.Equals("Wall"))
            {
                pushPullObjects.canDropObject = true;
                //Debug.Log("no");
            }
        }
    }
}
