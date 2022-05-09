using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRock : MonoBehaviour
{
    public GameObject rockdrick1;
    public GameObject rockdrick2;
    public GameObject apple;
    public GameObject normalRock;

    public static bool leftHouse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && leftHouse == false)
        {
            leftHouse = true;
        }
    }

    private void Start()
    {
        if (leftHouse)
        {
            rockdrick1.SetActive(false);
            rockdrick2.SetActive(false);
            apple.SetActive(false);
            normalRock.SetActive(true);
        }
    }
}
