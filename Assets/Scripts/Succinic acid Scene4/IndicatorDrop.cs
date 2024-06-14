using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorDrop : MonoBehaviour
{
    public GameObject Flask;
    private Vector3 offset;
    private bool isDragging = false;
    public GameObject dropper; // Assign your dropper GameObject in the inspector
    public GameObject dropPrefab; // Assign your drop prefab in the inspector
    public Vector3 targetPosition; // Set your target position in the inspector
    public Vector3 returnPosition; // Set your return position in the inspector
    public Vector3 dropOrigin; // Set your drop origin in the inspector
    public float returnDuration = 1.0f; // Set the duration of the return movement in the inspector
    private bool isMovingToTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D boxCollider = Flask.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isMovingToTarget) return;

        if (Vector3.Distance(dropper.transform.position, targetPosition) < 1.0f)
        {
            StartCoroutine(Movedropper());
        }
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

    IEnumerator Movedropper()
    {
        isMovingToTarget = true;

        // Move the dropper to the target position
        dropper.transform.position = targetPosition;

        // Wait for 4 seconds at the target position
        yield return new WaitForSeconds(4);

        // Instantiate the first drop at the drop origin
        Instantiate(dropPrefab, dropOrigin, Quaternion.identity);

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Instantiate the second drop at the drop origin
        Instantiate(dropPrefab, dropOrigin, Quaternion.identity);

        // Lerp the dropper's position from the target position to the return position over the specified duration
        float elapsedTime = 0;
        while (elapsedTime < returnDuration)
        {
            dropper.transform.position = Vector3.Lerp(targetPosition, returnPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the dropper ends up exactly at the return position
        dropper.transform.position = returnPosition;

        // Enable the BoxCollider2D when the dropper has returned to its position
        BoxCollider2D boxCollider = Flask.GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;

        isMovingToTarget = false;
    }
}