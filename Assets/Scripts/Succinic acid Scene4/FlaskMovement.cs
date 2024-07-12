using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskMovement : MonoBehaviour
{
    public GameObject Flask;
    private Vector3 offset;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {   
        // Calculate the offset between the touch position and the object's position
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        isDragging = true;
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // Update the object's position based on touch input
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}