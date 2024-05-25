using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation around its own axis
    public float tilt = 0f;
    public bool showTiltAxis = true; // Toggle for showing the tilt axis
    public float tiltAxisOffset = 0.5f; // Offset of the tilt axis line from the object's center

    private LineRenderer tiltAxisLine;

    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tilt);

        if (showTiltAxis)
        {
            // Create the tilt axis line as a child object
            GameObject tiltAxisObject = new GameObject("TiltAxisLine");
            tiltAxisObject.transform.SetParent(transform);
            tiltAxisObject.transform.localPosition = Vector3.zero;
            tiltAxisObject.transform.localRotation = Quaternion.identity;

            tiltAxisLine = tiltAxisObject.AddComponent<LineRenderer>();
            tiltAxisLine.positionCount = 2;
            tiltAxisLine.useWorldSpace = true; // World space
            tiltAxisLine.startWidth = 0.25f;
            tiltAxisLine.endWidth = 0.25f;
            tiltAxisLine.material = new Material(Shader.Find("Sprites/Default")); // Change material as needed
            tiltAxisLine.startColor = Color.red;
            tiltAxisLine.endColor = Color.red;
            UpdateTiltAxis();
        }
    }



    void Update()
    {
        // Rotate around its own axis
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Update the tilt axis line
        if (showTiltAxis)
        {
            UpdateTiltAxis();
        }
    }


    void UpdateTiltAxis()
    {
        Vector3 start = transform.position + transform.up * (transform.localScale.y * 0.3f + tiltAxisOffset);
        Vector3 end = transform.position - transform.up * (transform.localScale.y * 0.3f + tiltAxisOffset);

        tiltAxisLine.SetPosition(0, start);
        tiltAxisLine.SetPosition(1, end);
    }

    void OnDrawGizmos()
    {
        if (showTiltAxis)
        {
            Gizmos.color = Color.red;
            Vector3 start = transform.position + transform.up * (transform.localScale.y * 0.3f + tiltAxisOffset);
            Vector3 end = transform.position - transform.up * (transform.localScale.y * 0.3f + tiltAxisOffset);
            Gizmos.DrawLine(start, end);
        }
    }
}