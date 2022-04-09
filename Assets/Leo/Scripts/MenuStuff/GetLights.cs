using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GetLights : MonoBehaviour
{
    public Light2D pointLight, spriteLight;
    [HideInInspector]
    public Material glowMat, notGlowMat, hiddenMat;

    private void Awake()
    {
        glowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - Glow");
        notGlowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - NotGlow");
        hiddenMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - Hidden");
    }
}
