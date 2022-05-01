using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject mCam;
    public GameObject Player;

    void LateUpdate()
    {
        mCam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }
}
