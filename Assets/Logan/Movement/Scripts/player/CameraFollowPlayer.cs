using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public GameObject mCam;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        mCam = GameObject.Find("Main Camera");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mCam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);

    }
}
