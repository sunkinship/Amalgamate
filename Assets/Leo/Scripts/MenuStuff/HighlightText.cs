using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class HighlightText : Selectable
{
    private TextMeshProUGUI targetText;
    private Light2D pointLight;
    private Light2D spriteLight;

    private Material glowMat;
    private Material notGlowMat;

    private bool glowing;

    protected override void Awake()
    {
        pointLight = GameObject.Find("HornPointLight").GetComponent<Light2D>();
        spriteLight = GameObject.Find("HornSpriteLight").GetComponent<Light2D>();
        targetText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        glowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - Glow");
        notGlowMat = Resources.Load<Material>("TextMatVariants/PixelEmulator-xq08 SDF - NotGlow");
    }

    void Update()
    {

        if(Input.GetButton("Fire1") && IsHighlighted())
        {
            Debug.Log("kidufr");
        }

        if (IsHighlighted())
        {
            TurnOnGlow();
        }
        else 
        {
            if (glowing)
            {
                TurnOffGlow();
            }
        }
    }

    private void TurnOnGlow()
    {
        glowing = true;
        targetText.fontSharedMaterial = glowMat;
        pointLight.intensity = 1;
        spriteLight.intensity = 1;
    }

    private void TurnOffGlow()
    {
        glowing = false;
        targetText.fontSharedMaterial = notGlowMat;
        pointLight.intensity = 0;
        spriteLight.intensity = 0;
    }
}
