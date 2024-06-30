using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphUI : MonoBehaviour
{
    public Graph graph;
    public TMP_InputField xScaleInput;
    public TMP_InputField yScaleInput;
    public TMP_InputField xValuesInput;
    public TMP_InputField yValuesInput;
    public Button plotButton;
    public TMP_Text slopeText; // Reference to your TMP_Text component

    void Start()
    {
        plotButton.onClick.AddListener(OnPlotButtonClick);
    }

    void OnPlotButtonClick()
    {
        Debug.Log("Plot button clicked");

        if (!float.TryParse(xScaleInput.text, out float xScale) || !float.TryParse(yScaleInput.text, out float yScale))
        {
            Debug.LogError("Invalid scale input");
            return;
        }

        graph.SetScales(xScale, yScale);
        Debug.Log($"Scales set: X={xScale}, Y={yScale}");

        string[] xValues = xValuesInput.text.Split(',');
        string[] yValues = yValuesInput.text.Split(',');

        graph.dataPoints.Clear();
        for (int i = 0; i < Mathf.Min(xValues.Length, yValues.Length); i++)
        {
            if (float.TryParse(xValues[i].Trim(), out float x) && float.TryParse(yValues[i].Trim(), out float y))
            {
                graph.AddDataPoint(x, y);
                Debug.Log($"Added point: ({x}, {y})");
            }
            else
            {
                Debug.LogWarning($"Invalid input at index {i}");
            }
        }

        graph.PlotGraph();
        // Calculate and display the slope
        float slope = graph.CalculateSlope();
        slopeText.text = $"Slope: {slope:F2}"; // Display slope with 2 decimal places
    }
}