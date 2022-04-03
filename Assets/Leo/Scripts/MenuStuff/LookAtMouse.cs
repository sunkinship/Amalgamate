using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Camera cam;

    private void Update()
    {
        Vector3 mousePos = cam.WorldToScreenPoint(Input.mousePosition);
    }
}
