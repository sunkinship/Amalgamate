using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GetLights : MonoBehaviour
{
    [HideInInspector]
    public Material glowMat, notGlowMat;

    private void Awake()
    {
        glowMat = Resources.Load<Material>("TextMatVariants/MenuButtonGlow");
        notGlowMat = Resources.Load<Material>("TextMatVariants/UnlitPlayer");
    }
}
