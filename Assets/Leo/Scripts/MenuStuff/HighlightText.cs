using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighlightText : MonoBehaviour
{
    public GetLights lightMats;
    private Image image;

    public GameObject particles;
    public Image textImage;
    private static bool lampOn;

    private float maxBrightness = 0.4f;

    public Material mat;
    public Renderer render;
    private static float hornIntensity = 0;
    private static float hornChangeRate = 1f;

    public bool mainMenuButton;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (mainMenuButton)
        {
            MenuPlayerLight();
        }
    }

    private void MenuPlayerLight()
    {
        //Debug.Log("light");
        if (lampOn)
        {
            //Debug.Log("on");
            hornIntensity += hornChangeRate * Time.deltaTime;
        }
        else
        {
            //Debug.Log("off");
            hornIntensity -= hornChangeRate * Time.deltaTime;
        }

        hornIntensity = Mathf.Clamp(hornIntensity, 0, maxBrightness);

        mat.SetColor("Color_18748F50", Color.yellow * hornIntensity);
    }

    public void TurnOnGlow()
    {
        //Debug.Log("glow");
        if (mainMenuButton == false)
        {
            textImage.color = new Color32(247, 217, 127, 255);
        }
        else
        {
            lampOn = true;
            textImage.material = lightMats.glowMat;
            image.material = lightMats.glowMat;
        }
    }

    public void TurnOffGlow()
    {
        //Debug.Log("off");
        if (mainMenuButton == false)
        {
            textImage.color = Color.white;
        }
        else
        {
            lampOn = false;
            textImage.material = lightMats.notGlowMat;
            image.material = lightMats.notGlowMat;
        }
    }

    public void PlayEffect()
    {
        particles.GetComponent<ParticleSystem>().Play();
    }
}