using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureHandler : MonoBehaviour
{
    RawImage img;
    RenderTexture tex;
    RectTransform rect;

    public int x, y;

    // Use this for initialization
    void Awake()
    {
        //Get RawImage and RectTransform
        img = GetComponent<RawImage>();
        rect = GetComponent<RectTransform>();

        //Set size to match the texture
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);

        //Create the texture
        tex = new RenderTexture(x, y, 16, RenderTextureFormat.ARGB32);
        tex.Create();

        //Assign the target texture and output
        float cachedCameraAspect = Camera.main.aspect;
        Camera.main.targetTexture = tex;
        img.texture = tex;

        //Set camera aspect correctly?!?
        //Camera.main.aspect = cachedCameraAspect;
        //Camera.main.rect = new Rect(0, 0, x, y);
    }
}
