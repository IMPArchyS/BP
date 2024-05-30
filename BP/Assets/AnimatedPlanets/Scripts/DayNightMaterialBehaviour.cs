using UnityEngine;
using System.Collections;

public class DayNightMaterialBehaviour : MonoBehaviour 
{
    public Light Sun;

    private new Renderer renderer;
    private new Light light;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        light = Sun.GetComponent<Light>();
    }

    void Update()
    {
        if (light.type == LightType.Point)
        {
            renderer.material.SetVector("_SunDir", Sun.transform.position - transform.position);
        }
        else //if (Sun.light.type == LightType.Directional)
        {
            renderer.material.SetVector("_SunDir", -Sun.transform.forward);
        }
    }

    void OnDestroy()
    {
        if (renderer.material)
        {
            DestroyImmediate(renderer.material);
        }
    }
}
