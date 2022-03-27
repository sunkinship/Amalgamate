using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollowPlayer : MonoBehaviour
{

    public GameObject mCam;
    private GameObject Player;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mCam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }
}
