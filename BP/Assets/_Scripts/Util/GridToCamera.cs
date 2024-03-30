using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridToCamera : MonoBehaviour
{
    [SerializeField] private Transform camObject;
    [SerializeField] float offsetX = 1;
    [SerializeField] float offsetZ = 1;
    private void Awake()
    {
        camObject = Camera.main.transform;
    }

    private void Update()
    {
        float minX = Camera.main.nearClipPlane;
        float maxX = Camera.main.farClipPlane;
        float minZ = Camera.main.nearClipPlane;
        float maxZ = Camera.main.farClipPlane;

        float clampedX = Mathf.Clamp(camObject.position.x + offsetX, minX, maxX);
        float clampedZ = Mathf.Clamp(camObject.position.z + offsetZ, minZ, maxZ);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
