using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script is for small beaker.
public class EmptySmallBeaker : MonoBehaviour
{
  private Vector3 offset;
  private bool isDragging = false;
  public GameObject parentBottle; // Add this line to reference the parent bottle
  public Vector3 targetPosition = new Vector3(-1.64f, 1.92f, 0f);
  public float thresholdDistance = 2.0f;
  public bool hasReachedTarget = false;
  public SpriteRenderer bottleMaskSR;
  public AnimationCurve FillAmountCurve;
  public SpriteRenderer otherBottleSR; // Reference to the second bottle
  public AnimationCurve OtherBottleFillAmountCurve; // New AnimationCurve for the other bottle
    //create two more Animation curves
  public AnimationCurve OtherBottleFillAmountCurve2;
  private float timer = 0.0f;
  public bool isRotated = false; // Add this line to track if the bottle has been rotated
  public bool hasRotatedOnce = false;
  public LineRenderer lineRenderer;
  public Transform bottleOpening; // Add this line
  public Transform waterPouringInto; // Add this line
  public bool isRotating = false; // Add this line to track if the bottle is rotating
  public GameObject myButtonPrefab; // Reference to the button prefab
  public static bool isSmallBeakerPickedFirst = false;
  public static bool animationEnded = false;
  public AudioSource rotationSound;

  // Start is called before the first frame update
  void Start()
  {
    Screen.orientation = ScreenOrientation.LandscapeLeft;
  }
  // Update is called once per frame
  void Update()
  {
    AnimationCurve otherCurveToUse = EmptyLargeBeaker.isLargeBeakerPickedFirst ? OtherBottleFillAmountCurve2 : OtherBottleFillAmountCurve;
    if (hasReachedTarget && !hasRotatedOnce)
    {
        // Rotate the bottle by 41 degrees in the z-axis
        parentBottle.transform.rotation = Quaternion.Euler(0, 0, -90);
        // Start the coroutine to wait for 5 seconds
        StartCoroutine(WaitAndResetRotation());
        hasReachedTarget = false; // Reset the flag
        isRotated = true; // Set the flag to true
        hasRotatedOnce = true; // Set the flag to true
        isRotating = true; // Set the flag to true
    }
    
    if (isRotating)
      {
          // Use the FillAmountCurve to control the fill amount of the bottle
          float fillAmount = FillAmountCurve.Evaluate(timer);
          bottleMaskSR.material.SetFloat("_WaterLevel", fillAmount);

          // Use the OtherBottleFillAmountCurve to control the fill amount of the other bottle
          float otherFillAmount = otherCurveToUse.Evaluate(timer);
          otherBottleSR.material.SetFloat("_WaterLevel", otherFillAmount);

          timer += Time.deltaTime; // Increase the timer
          UpdateLineRenderer(); // Add this line to update the LineRenderer
      }
      // Check if the animations in both scripts have ended
        if (EmptySmallBeaker.animationEnded && EmptyLargeBeaker.animationEnded)
        {
            // Instantiate the button
            InstantiateButton();

            // Reset the variables
            EmptySmallBeaker.animationEnded = false;
            EmptyLargeBeaker.animationEnded = false;
        }   
  }
  void OnAnimationEnd()
    {
        animationEnded = true;
    }
    private void InstantiateButton()
  {
    Vector3 worldPosition = new Vector3(7.343f, 3.509f, 0f);
    GameObject button = Instantiate(myButtonPrefab);

    // Find the Canvas in your scene
    Canvas canvas = FindObjectOfType<Canvas>();

    // If a Canvas exists, set it as the parent of the button
    if (canvas != null)
    {
      button.transform.SetParent(canvas.transform, false);
    }

    // Convert world position to Canvas position
    Vector2 canvasPosition;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(worldPosition), null, out canvasPosition);

    // Set the position of the button
    button.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
    // Get the Button component from the instantiated button
    
    Button buttonComponent = button.GetComponent<Button>();
    // Add an onClick event to the button that calls the LoadScene3 method
        buttonComponent.onClick.AddListener(LoadScene4);
}
private void LoadScene4()
    {
        // Load Scene3
        SceneManager.LoadScene("Scene4");
    }
  void UpdateLineRenderer()
    {
        // Set the first position of the LineRenderer to the position of the bottle's opening
        lineRenderer.SetPosition(0, bottleOpening.position);

        // Set the second position of the LineRenderer to the position where the water is pouring into
        lineRenderer.SetPosition(1, waterPouringInto.position);
    }
  IEnumerator WaitAndResetRotation()
{
    // Wait for 5 seconds
    rotationSound.Play(); // Play the rotation sound
    yield return new WaitForSeconds(4);
    rotationSound.Stop(); // Stop the rotation sound
    // Reset the rotation
    parentBottle.transform.rotation = Quaternion.Euler(0, 0, 0);
    isRotated = false; // Reset the flag
    timer = 0.0f; // Reset the timer
    isRotating = false; // Reset the flag
    lineRenderer.enabled = false;
    OnAnimationEnd();
}
  private void OnMouseDown()
    {
        isSmallBeakerPickedFirst = true;
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

            // Check the distance between the new position and the target
            if (Vector3.Distance(newPosition, targetPosition) < thresholdDistance)
            {
                // If the distance is less than the threshold, snap the position to the target
                parentBottle.transform.position = targetPosition;
                hasReachedTarget = true; // Set the flag to true

                // Evaluate the curve at the current time
            }
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
        if (!isRotated)
        {
            // Reset the rotation
            parentBottle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}