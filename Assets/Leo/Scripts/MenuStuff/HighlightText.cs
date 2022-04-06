using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightText : MonoBehaviour
{
    [HideInInspector]
    public GetLights getLights;
    private TextMeshProUGUI targetText;
    public GameObject particles;
    private static bool isHighlighted;
    private static float intensity = 0;
    private static float changeRate = 0.5f;

    private void Awake()
    {
        targetText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        getLights = GameObject.Find("HandleButtonEvents").GetComponent<GetLights>();
    }

    private void Update()
    {
        if (isHighlighted)
        {
            intensity += changeRate * Time.deltaTime;
        }
        else
        {
            intensity -= changeRate * Time.deltaTime;
        }

        intensity = Mathf.Clamp(intensity, 0, 0.6f);

        getLights.pointLight.intensity = intensity;
        getLights.spriteLight.intensity = intensity;
    }

    public void TurnOnGlow()
    {
        isHighlighted = true;
        targetText.fontSharedMaterial = getLights.glowMat;
        //getLights.pointLight.intensity = 0.5f;
        //getLights.spriteLight.intensity = 0.7f;
    }

    public void TurnOffGlow()
    {
        isHighlighted = false;
        targetText.fontSharedMaterial = getLights.notGlowMat;
        //getLights.pointLight.intensity = 0;
        //getLights.spriteLight.intensity = 0;
    }

    public void IncreaseGlow()
    {
        targetText.fontSharedMaterial = getLights.glowMat;
        getLights.pointLight.intensity = 0.7f;
        getLights.spriteLight.intensity = 1f;
    }

    public void PlayEffect()
    {
        particles.GetComponent<ParticleSystem>().Play();
    }
}