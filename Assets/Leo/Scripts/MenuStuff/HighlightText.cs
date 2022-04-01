using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

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
        //CheckMouse();
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

    private void OnMouseDown()
    {
        targetText.fontSharedMaterial = glowMat;
        pointLight.intensity = 2;
        spriteLight.intensity = 2;
        Debug.Log("Mouse Down Method");
    }

    private void OnMouseUp()
    {
        targetText.fontSharedMaterial = notGlowMat;
        pointLight.intensity = 0;
        spriteLight.intensity = 0;
        Debug.Log("Mouse Up Method");
    }

    private void CheckMouse()
    {
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        switch (e.GetTypeForControl(controlID))
        {
            case EventType.MouseDown:
                Debug.Log("Mouse Down Case");
                break;
            case EventType.MouseUp:
                Debug.Log("Mouse Up Case");
                break;
        }
    }
}