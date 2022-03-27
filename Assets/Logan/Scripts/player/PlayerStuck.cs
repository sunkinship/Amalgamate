using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuck : MonoBehaviour
{
    private float checkTime = 0.00001f;
    private Vector2 oldPos;
    public MapMove mapIcon;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTime <= 0)
        {
            oldPos = transform.position;
            checkTime = 0.00001f;
        }
        else
        {
            checkTime -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, oldPos) < 0.01f)
        {
            mapIcon.speed = 0;
        }
        else
        {
            mapIcon.speed = 2;
        }
    }
}
