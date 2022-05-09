using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighlightText : MonoBehaviour
{
    public GetLights getLights;
    public GameObject particles;
    public Image textImage;
    //public Image borderImage;
    //[HideInInspector]
    //public TextMeshProUGUI targetText;
    //private static bool isHighlighted;
    //private static float intensity = 0;
    //private static float changeRate = 0.5f;
    //private static float maxBrightness = 0.4f;
    //public bool hornGlow;

    private void Awake()
    {
        //targetText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //if (hornGlow)
        //{
        //    if (isHighlighted)
        //    {
        //        intensity += changeRate * Time.deltaTime;
        //    }
        //    else
        //    {
        //        intensity -= changeRate * Time.deltaTime;
        //    }

        //    intensity = Mathf.Clamp(intensity, 0, maxBrightness);

        //    getLights.pointLight.intensity = intensity;
        //    getLights.spriteLight.intensity = intensity;
        //}
    }

    public void TurnOnGlow()
    {
        textImage.color = new Color32(247, 217, 127, 255);
        //borderImage.color = new Color32(247, 217, 127, 255);
        //isHighlighted = true;
        //targetText.fontSharedMaterial = getLights.glowMat;
    }

    //public void TurnOnGlow2()
    //{
    //    textImage.color = new Color32(247, 217, 127, 255);
    //    //borderImage.color = new Color32(247, 217, 127, 255);
    //    //isHighlighted = true;
    //    //targetText.fontSharedMaterial = getLights.notButtonMat;
    //}

    public void TurnOffGlow()
    {
        textImage.color = Color.white;
        //borderImage.color = Color.white;
        //isHighlighted = false;
        //targetText.fontSharedMaterial = getLights.notGlowMat;
    }

    public void IncreaseGlow()
    {
        //targetText.fontSharedMaterial = getLights.glowMat;
        //getLights.pointLight.intensity = 0.7f;
        //getLights.spriteLight.intensity = 1f;
    }

    public void PlayEffect()
    {
        particles.GetComponent<ParticleSystem>().Play();
    }
}