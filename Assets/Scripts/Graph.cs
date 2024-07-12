using UnityEngine;
using System.Collections.Generic;

public class Graph : MonoBehaviour
{
    public RectTransform graphArea;
    public LineRenderer lineRenderer;
    public float xScale = 1f;
    public float yScale = 1f;
    public List<Vector3> dataPoints = new List<Vector3>();
    private float minX, maxX, minY, maxY;
    private float xRange, yRange;

    [Header("Graph Positioning")]
    public Vector2 graphOffset = Vector2.zero;
    public Vector2 graphSize = new Vector2(100, 100);

    private void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not assigned!");
            return;
        }

        SetupLineRenderer();
    }

    private void SetupLineRenderer()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
    }

    public void SetScales(float newXScale, float newYScale)
    {
        xScale = Mathf.Max(newXScale, 0.01f); // Prevent division by zero
        yScale = Mathf.Max(newYScale, 0.01f);
    }

    public void AddDataPoint(float x, float y)
    {
        dataPoints.Add(new Vector3(x, y, 0));
    }

    public void PlotGraph()
    {
        if (dataPoints.Count < 2)
        {
            Debug.LogWarning("Not enough data points to plot a line.");
            return;
        }

        lineRenderer.positionCount = dataPoints.Count;

        minX = float.MaxValue;
        maxX = float.MinValue;
        minY = float.MaxValue;
        maxY = float.MinValue;

        foreach (Vector3 point in dataPoints)
        {
            minX = Mathf.Min(minX, point.x);
            maxX = Mathf.Max(maxX, point.x);
            minY = Mathf.Min(minY, point.y);
            maxY = Mathf.Max(maxY, point.y);
        }

        float xRange = maxX - minX;
        float yRange = maxY - minY;

        if (xRange == 0) xRange = 1; // Prevent division by zero
        if (yRange == 0) yRange = 1;

        for (int i = 0; i < dataPoints.Count; i++)
        {
            Vector2 normalizedPoint = new Vector2(
                (dataPoints[i].x - minX) / xRange,
                (dataPoints[i].y - minY) / yRange
            );

            Vector3 localPosition = new Vector3(
                graphOffset.x + normalizedPoint.x * graphSize.x,
                graphOffset.y + normalizedPoint.y * graphSize.y,
                0
            );

            Vector3 worldPosition = transform.TransformPoint(localPosition);

            if (float.IsInfinity(worldPosition.x) || float.IsInfinity(worldPosition.y) || float.IsNaN(worldPosition.x) || float.IsNaN(worldPosition.y))
            {
                Debug.LogWarning($"Invalid position calculated for point {i}: {worldPosition}");
                worldPosition = Vector3.zero;
            }

            lineRenderer.SetPosition(i, worldPosition);
        }

        Debug.Log($"Plotted {dataPoints.Count} points");
    }
    public float CalculateSlope()
    {
        if (dataPoints.Count < 2)
        {
            return 0;
        }

        // Use the first and last points to calculate the slope
        Vector3 firstPoint = dataPoints[0];
        Vector3 lastPoint = dataPoints[dataPoints.Count - 1];

        float deltaY = (lastPoint.y - firstPoint.y) / yScale;
        float deltaX = (lastPoint.x - firstPoint.x) / xScale;

        if (deltaX == 0)
        {
            return float.PositiveInfinity; // Vertical line
        }

        return deltaY / deltaX;
    }
    public void UpdateGraphPositions()
    {
        if (dataPoints.Count < 2) return;

        Vector3 scale = transform.localScale;
        
        for (int i = 0; i < dataPoints.Count; i++)
        {
            Vector2 normalizedPoint = new Vector2(
                (dataPoints[i].x - minX) / xRange,
                (dataPoints[i].y - minY) / yRange
            );

            Vector3 localPosition = new Vector3(
                (graphOffset.x + normalizedPoint.x * graphSize.x) / scale.x,
                (graphOffset.y + normalizedPoint.y * graphSize.y) / scale.y,
                0
            );

            Vector3 worldPosition = transform.TransformPoint(localPosition);

            if (float.IsInfinity(worldPosition.x) || float.IsInfinity(worldPosition.y) || 
                float.IsNaN(worldPosition.x) || float.IsNaN(worldPosition.y))
            {
                worldPosition = Vector3.zero;
            }

            lineRenderer.SetPosition(i, worldPosition);
        }
    }
}