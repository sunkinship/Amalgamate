using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnable : MonoBehaviour
{
    public GameObject Map;
    private bool mapEnabled;
    public playerMovement pM;
    // Start is called before the first frame update
    void Start()
    {
        mapEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapEnabled == false && Input.GetKeyDown(KeyCode.M))
        {
            Map.SetActive(true);
            mapEnabled = true;
            pM.moveSpeed = 0;


        } else if (mapEnabled == true && Input.GetKeyDown(KeyCode.M)){

            Map.SetActive(false);
            mapEnabled = false;
            pM.moveSpeed = 6;

        }
    }
}

