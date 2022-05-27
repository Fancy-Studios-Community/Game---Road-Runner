using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTrack : MonoBehaviour
{
    public float speed;
    private Vector2 offset;
    Renderer backgroundRenderer;
    void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += new Vector2(0, Time.deltaTime * speed);
        backgroundRenderer.material.mainTextureOffset = offset;
    }
}
