using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitalMovement : MonoBehaviour
{
    public Transform centerObject;
    public Vector3 centerOffset = Vector3.zero;
    public float orbitSpeed = 10f;
    public float xRadius = 5f;
    public float yRadius = 3f;
    public float zRadius = 4f;
    public float tiltAngle = 30f;
    public bool showOrbit = true;

    private LineRenderer lineRenderer;
    private readonly int segments = 100;

    private float currentTime = 0f;
    private Vector3 offset = Vector3.zero;

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

        // Initialize currentTime with a random value between 0 and 2π
        currentTime = Random.value * 2 * Mathf.PI;
        UpdateOrbitPath();
    }

    private void Update()
    {
        int stellarTimeScale;
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        currentTime += Time.deltaTime * stellarTimeScale;

        float orbitPeriod = 2 * Mathf.PI / orbitSpeed;
        currentTime %= orbitPeriod;

        float angle = currentTime * orbitSpeed;

        float x = Mathf.Cos(angle) * xRadius;
        float y = Mathf.Sin(angle) * yRadius;
        float z = Mathf.Sin(angle) * zRadius;

        offset = new(x, y, z);
        Quaternion tiltRotation = Quaternion.Euler(tiltAngle, 0, 0);
        offset = tiltRotation * offset;

        if (showOrbit)
        {
            UpdateOrbitPath();
        }
        lineRenderer.enabled = showOrbit;
    }

    private void LateUpdate()
    {
        transform.position = centerObject.position + centerOffset + offset;
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