using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMask : MonoBehaviour
{
    public SpriteMask mask;

    public SpriteRenderer targetRenderer;

    private void LateUpdate()
    {
        if (mask.sprite != targetRenderer)
        {
            mask.sprite = targetRenderer.sprite;
        }
    }
}
