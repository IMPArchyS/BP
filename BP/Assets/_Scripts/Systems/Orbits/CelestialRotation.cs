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
            GameObject tiltAxisObject = new("TiltAxisLine");
            tiltAxisObject.transform.SetParent(transform);
            tiltAxisObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            tiltAxisLine = tiltAxisObject.AddComponent<LineRenderer>();
            tiltAxisLine.positionCount = 2;
            tiltAxisLine.useWorldSpace = true; // World space
            tiltAxisLine.startWidth = 0.25f;
            tiltAxisLine.startColor = new Color32(126, 126, 126, 255);
            tiltAxisLine.endColor = new Color32(126, 126, 126, 255);
            tiltAxisLine.endWidth = 0.25f;
            tiltAxisLine.material = new Material(Resources.Load<Material>("Shaders/Lines"));
            tiltAxisLine.startColor = Color.red;
            tiltAxisLine.endColor = Color.red;
            UpdateTiltAxis();
        }
    }

    private void OnValidate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, tilt);
    }

    void Update()
    {
        int stellarTimeScale;
        // Increment the angle based on time and speed
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * stellarTimeScale, Space.World);

        // Update the tilt axis line
        if (showTiltAxis)
        {
            UpdateTiltAxis();
        }
    }


    void UpdateTiltAxis()
    {
        Vector3 start = transform.position + transform.up * (transform.localScale.y * 0.7f + tiltAxisOffset);
        Vector3 end = transform.position - transform.up * (transform.localScale.y * 0.7f + tiltAxisOffset);

        tiltAxisLine.SetPosition(0, start);
        tiltAxisLine.SetPosition(1, end);
    }

    void OnDrawGizmos()
    {
        if (showTiltAxis)
        {
            Gizmos.color = Color.red;
            Vector3 start = transform.position + transform.up * (transform.localScale.y * 0.7f + tiltAxisOffset);
            Vector3 end = transform.position - transform.up * (transform.localScale.y * 0.7f + tiltAxisOffset);
            Gizmos.DrawLine(start, end);
        }
    }
}