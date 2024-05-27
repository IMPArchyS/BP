using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitalMovement : MonoBehaviour
{
    public Transform centerObject; // Object to orbit around
    public Vector3 centerOffset = Vector3.zero; // Offset to the center object
    public float orbitSpeed = 10f; // Speed of the orbit
    public float xRadius = 5f; // Radius on the x-axis
    public float yRadius = 3f; // Radius on the y-axis
    public float zRadius = 4f; // Radius on the z-axis
    public float tiltAngle = 30f; // Tilt angle for the orbit plane
    public bool showOrbit = true; // Toggle for showing the orbit path

    private LineRenderer lineRenderer;
    private readonly int segments = 100;

    private float currentTime = 0f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startColor = new Color32(126, 126, 126, 255);
        lineRenderer.endColor = new Color32(126, 126, 126, 255);
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.material = new Material(Resources.Load<Material>("Shaders/Lines"));
        UpdateOrbitPath();
    }

    private void Update()
    {
        int stellarTimeScale;
        // Increment the angle based on time and speed
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        // Increment the current time based on the custom timescale
        currentTime += Time.deltaTime * stellarTimeScale;

        // Calculate the normalized time within a single orbit period
        float orbitPeriod = 2 * Mathf.PI / orbitSpeed;
        currentTime %= orbitPeriod;

        // Calculate the angle based on normalized time and orbit speed
        float angle = currentTime * orbitSpeed;

        // Calculate the new position in an elliptical path
        float x = Mathf.Cos(angle) * xRadius;
        float y = Mathf.Sin(angle) * yRadius;
        float z = Mathf.Sin(angle) * zRadius;

        // Apply tilt to the orbit
        Vector3 offset = new(x, y, z);
        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0, 0);
        offset = tiltRotation * offset;

        // Set the position of the orbiting object
        transform.position = centerObject.position + centerOffset + offset;

        // Update the orbit path if toggled
        if (showOrbit)
        {
            UpdateOrbitPath();
        }
        lineRenderer.enabled = showOrbit;
    }

    private void UpdateOrbitPath()
    {
        float theta = 0;
        float increment = 2.0f * Mathf.PI / segments;

        for (int i = 0; i <= segments; i++)
        {
            Vector3 point = CalculateEllipsePoint(theta);
            lineRenderer.SetPosition(i, centerObject.position + centerOffset + point);
            theta += increment;
        }
    }

    private Vector3 CalculateEllipsePoint(float theta)
    {
        float x = Mathf.Cos(theta) * xRadius;
        float y = Mathf.Sin(theta) * yRadius;
        float z = Mathf.Sin(theta) * zRadius;
        Vector3 point = new(x, y, z);
        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0, 0);
        return tiltRotation * point;
    }

    private void OnDrawGizmos()
    {
        if (centerObject != null && showOrbit)
        {
            Gizmos.color = Color.yellow;
            float theta = 0;
            float increment = 2.0f * Mathf.PI / segments;

            Vector3 previousPoint = CalculateEllipsePoint(theta);
            for (int i = 1; i <= segments; i++)
            {
                theta += increment;
                Vector3 nextPoint = CalculateEllipsePoint(theta);
                Gizmos.DrawLine(centerObject.position + centerOffset + previousPoint, centerObject.position + centerOffset + nextPoint);
                previousPoint = nextPoint;
            }
        }
    }
}