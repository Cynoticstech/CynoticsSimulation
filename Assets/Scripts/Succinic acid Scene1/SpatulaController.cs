using System.Collections;
using TMPro;
using UnityEngine;

public class SpatulaController : MonoBehaviour
{
  public Transform spatula; // Reference to the spatula GameObject
  public Vector2 mouthPosition; // Position near the mouth of the box
  public GameObject powderSprite; // Reference to the powder game object
  private Vector2 originalPosition; // Original position of the spatula
  private Vector3 offset; // Offset for dragging
  private bool isDragging; // Flag to track dragging state
  

  private void Start()
  {
    originalPosition = spatula.position;
    powderSprite.SetActive(false); // Initially hide the powder sprite
  }

  public void MoveToMouth()
  {
    // Reset powder state (optional)
    powderSprite.SetActive(false);
    powderSprite.GetComponent<Rigidbody2D>().isKinematic = true;

    // Move the spatula to the mouth position
    StartCoroutine(MoveSpatula(mouthPosition));
  }

  private IEnumerator MoveSpatula(Vector2 targetPosition)
  {
    float elapsedTime = 0f;
    float moveDuration = 2f; // Duration for the movement

    while (elapsedTime < moveDuration)
    {
      float t = Mathf.Clamp01(elapsedTime / moveDuration); // Normalize the time
      spatula.position = Vector2.Lerp(originalPosition, targetPosition, t);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    // Ensure the spatula reaches exactly the target position
    spatula.position = targetPosition;

    // Set the position of the powder sprite relative to the spatula (at mouth)
    powderSprite.transform.position = spatula.position + new Vector3(1.05f, -0.37f, 0f);

    // Activate powder sprite at mouth position
    powderSprite.SetActive(true);

    // Automatically return to the original position
    StartCoroutine(ReturnToOriginal());
  }

  private IEnumerator ReturnToOriginal()
  {
    float elapsedTime = 0f;
    float returnDuration = 2f; // Duration for the return movement

    while (elapsedTime < returnDuration)
    {
      float t = Mathf.Clamp01(elapsedTime / returnDuration); // Normalize the time
      spatula.position = Vector2.Lerp(mouthPosition, originalPosition, t);

      // Move powder sprite along with spatula during return
      powderSprite.transform.position = spatula.position + new Vector3(1.05f, -0.37f, 0f);

      elapsedTime += Time.deltaTime;
      yield return null;
    }

    // Ensure spatula reaches original position
    spatula.position = originalPosition;

    // Keep powder sprite inactive even after returning (optional)
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

      // Move powder sprite along with spatula during dragging ONLY if not dropped
      if (!powderSprite.GetComponent<Rigidbody2D>().isKinematic)
      {
        return; // Exit the function if powder has dropped
      }

      powderSprite.transform.position = spatula.position + new Vector3(1.05f, -0.37f, 0f);

      // Check if the spatula is close to the target coordinates
      float distanceToTarget = Vector2.Distance(spatula.position, new Vector2(-8.11f, 1.02f));
      if (distanceToTarget < 0.5f) // Adjust the threshold as needed
      {
        // Trigger the powder drop (you can add rigidbody activation here)
        powderSprite.GetComponent<Rigidbody2D>().isKinematic = false;
      }
    }
  }
}
