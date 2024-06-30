using UnityEngine;

public class GraphZoom : MonoBehaviour
{
    public RectTransform graphPanel;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    private Vector3 initialScale;
    private float initialDistance;
    public Graph graph;

    void Start()
    {
        initialScale = graphPanel.localScale;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = graphPanel.localScale;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float scaleFactor = currentDistance / initialDistance;

                Vector3 newScale = initialScale * scaleFactor;
                newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
                newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
                newScale.z = 1;

                graphPanel.localScale = newScale;
                graph.UpdateGraphPositions();
            }
        }
    }
}
