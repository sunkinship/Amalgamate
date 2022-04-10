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
    private static float maxBrightness = 0.4f;

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

        intensity = Mathf.Clamp(intensity, 0, maxBrightness);

        getLights.pointLight.intensity = intensity;
        getLights.spriteLight.intensity = intensity;
    }

    public void TurnOnGlow()
    {
        isHighlighted = true;
        targetText.fontSharedMaterial = getLights.glowMat;
    }

    public void TurnOnGlow2()
    {
        isHighlighted = true;
        targetText.fontSharedMaterial = getLights.notButtonMat;
    }

    public void TurnOffGlow()
    {
        isHighlighted = false;
        targetText.fontSharedMaterial = getLights.notGlowMat;
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