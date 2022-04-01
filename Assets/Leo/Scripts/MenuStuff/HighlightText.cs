using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightText : MonoBehaviour
{
    [HideInInspector]
    public GetLights getLights;
    private TextMeshProUGUI targetText;

    private void Awake()
    {
        targetText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        getLights = GameObject.Find("HandleButtonEvents").GetComponent<GetLights>();
    }

    public void TurnOnGlow()
    {
        //Debug.Log("Entered");
        targetText.fontSharedMaterial = getLights.glowMat;
        getLights.pointLight.intensity = 0.5f;
        getLights.spriteLight.intensity = 0.7f;
    }

    public void TurnOffGlow()
    {
        //Debug.Log("Exited");
        targetText.fontSharedMaterial = getLights.notGlowMat;
        getLights.pointLight.intensity = 0;
        getLights.spriteLight.intensity = 0;
    }

    public void IncreaseGlow()
    {
        //Debug.Log("Pressed");
        targetText.fontSharedMaterial = getLights.glowMat;
        getLights.pointLight.intensity = 0.7f;
        getLights.spriteLight.intensity = 1f;
    }
}