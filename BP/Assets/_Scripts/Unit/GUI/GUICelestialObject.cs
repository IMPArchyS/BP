using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICelestialObject : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 10f;
    public float tiltAngle = 30f;

    void OnGUI()
    {
        // Calculate the position for the GUI element
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        float radius = 100f;

        // Draw a circle
        GUI.DrawTexture(new Rect(center.x - radius, center.y - radius, radius * 2, radius * 2), Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 10.0f);

        // Draw arrows for rotation
        Vector2 arrowStart = center + new Vector2(Mathf.Cos(tiltAngle * Mathf.Deg2Rad), Mathf.Sin(tiltAngle * Mathf.Deg2Rad)) * radius;
        Vector2 arrowEnd = center + new Vector2(Mathf.Cos((tiltAngle + 180f) * Mathf.Deg2Rad), Mathf.Sin((tiltAngle + 180f) * Mathf.Deg2Rad)) * radius;

        DrawArrow(arrowStart, arrowEnd, Color.red);

        // Draw a line for tilt
        Vector2 lineStart = center + new Vector2(-radius, 0);
        Vector2 lineEnd = center + new Vector2(radius, 0);

        Debug.DrawLine(lineStart, lineEnd, Color.blue);
    }

    // Helper method to draw an arrow
    void DrawArrow(Vector2 start, Vector2 end, Color color)
    {
        Vector2 direction = (end - start).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);

        float arrowSize = 10f;

        // Draw arrow body
        GUI.color = color;
        Debug.DrawLine(start, end);

        // Draw arrow head
        Vector2 arrowTip = end - direction * arrowSize;
        Vector2 arrowLeft = arrowTip + perpendicular * arrowSize * 0.5f;
        Vector2 arrowRight = arrowTip - perpendicular * arrowSize * 0.5f;
        Debug.DrawLine(arrowLeft, arrowTip);
        Debug.DrawLine(arrowRight, arrowTip);
    }
}
