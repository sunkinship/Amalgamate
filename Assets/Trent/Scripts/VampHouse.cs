using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampHouse : MonoBehaviour
{
    public fastTravel fastT;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(fastT.Fade());
        }
    }
}

