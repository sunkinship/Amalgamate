using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GetLights : MonoBehaviour
{
    [HideInInspector]
    public Light2D pointLight, spriteLight;
    [HideInInspector]
    public Material glowMat, notGlowMat, hiddenMat;

    private void Awake()
    {
        pointLight = GameObject.Find("HornPointLight").GetComponent<Light2D>();
        spriteLight = GameObject.Find("HornSpriteLight").GetComponent<Light2D>();
        glowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - Glow");
        notGlowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - NotGlow");
        hiddenMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - Hidden");
    }
}
