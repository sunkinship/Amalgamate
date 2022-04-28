using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNPCsDirections : MonoBehaviour
{
    public bool right;
    public bool left;
    public SpriteRenderer spi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(right == true)
            {
                spi.flipX = false;
            }
            if(left == true)
            {
                spi.flipX = true;
            }
        }
    }
}
