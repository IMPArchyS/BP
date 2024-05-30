using UnityEngine;
using System.Collections;

public class AnimatingMatBehaviour : MonoBehaviour 
{
    public float loopduration = 1;
    public float loopoffset = 0;
    public bool animate = true;

    private new Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (!animate)
        {
            SetAnimationPosition(0);
        }
    }

    void Update()
    {
        if (animate)
        {
            SetAnimationPosition(Time.time);
        }
    }

    void SetAnimationPosition( float time )
    {
        float r = Mathf.Sin((time / loopduration + loopoffset) * (2 * Mathf.PI)) * 0.5f + 0.25f;
        float g = Mathf.Sin((time / loopduration + loopoffset + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
        float b = Mathf.Sin((time / loopduration + loopoffset + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
        float correction = 1.0f / (r + g + b);
        r *= correction;
        g *= correction;
        b *= correction;
        renderer.material.SetVector("_ChannelFactor", new Vector4(r, g, b, 0));
    }

    void OnDestroy()
    {
        if (renderer.material)
        {
            DestroyImmediate(renderer.material);
        }
    }
}
