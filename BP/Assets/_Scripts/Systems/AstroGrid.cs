using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroGrid : MonoBehaviour
{
    public float scale = 1f; // Custom scale for the grid
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (float x = 0; x <= scale; x++)
        {
            // Create vertical lines
            GameObject verticalLine = new GameObject("VerticalLine");
            verticalLine.transform.parent = transform;
            LineRenderer verticalLineRenderer = verticalLine.AddComponent<LineRenderer>();
            verticalLineRenderer.positionCount = 2;
            verticalLineRenderer.SetPosition(0, new Vector3(x, 0, 0));
            verticalLineRenderer.SetPosition(1, new Vector3(x, scale, 0));
        }

        for (float y = 0; y <= scale; y++)
        {
            // Create horizontal lines
            GameObject horizontalLine = new GameObject("HorizontalLine");
            horizontalLine.transform.parent = transform;
            LineRenderer horizontalLineRenderer = horizontalLine.AddComponent<LineRenderer>();
            horizontalLineRenderer.positionCount = 2;
            horizontalLineRenderer.SetPosition(0, new Vector3(0, y, 0));
            horizontalLineRenderer.SetPosition(1, new Vector3(scale, y, 0));
        }
    }
}
