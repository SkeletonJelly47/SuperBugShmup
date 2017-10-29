using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    Renderer renderer;
    Vector2 vec;
    float scrollSpeed;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Debug.Log(renderer);
        vec = new Vector2(0, 0);
        scrollSpeed = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        vec.y -= scrollSpeed * Time.deltaTime;

        renderer.material.mainTextureOffset = vec;
    }
}
