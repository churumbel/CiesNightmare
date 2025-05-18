using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.015f;
    private Renderer bgRenderer;

    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (bgRenderer != null)
        {
            bgRenderer.material.mainTextureOffset += new Vector2(scrollSpeed, 0) * Time.deltaTime;
        }
    }
}
