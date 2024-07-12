using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StirrerBehaviour : MonoBehaviour
{
    private Vector3 offset;
    public GameObject myButtonPrefab; // The button prefab
    public UnityEvent onStirrerAnimationStart;
    public bool isAnimating = false; // Add this line to track if the stirrer is animating
    private bool isDragging = false;
    public AnimationCurve movementCurveX;
    public AnimationCurve movementCurveY;
    private Vector3 targetPosition = new Vector3(-7.62f, -1.66f, 0f); // Set the z value according to your needs
    private float timeX = 0f;
    private float timeY = 0f;
    public ParticleSystem secondParticleSystem; // The second particle system
    private bool buttonInstantiated = false;
    private void OnMouseDown()
    {
        // Calculate the offset between the touch position and the object's position
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        isDragging = true;
    }
    private void StartAnimation()
        {
            isAnimating = true; // Set this to true when the animation starts
        }
    private void InstantiateButton()
    {
        Vector3 worldPosition = new Vector3(8.85f, 3.84f, 0f);
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
        buttonComponent.onClick.AddListener(LoadScene3);
    }
    private void LoadScene3()
    {
        // Load Scene3
        SceneManager.LoadScene("Scene3");
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

    private void Update()
    {
        // Check if the stirrer is close to the target position
        if (Vector3.Distance(transform.position, targetPosition) < 1.0f)
        {
            // If it is, start dragging and move the stirrer to the target position
            isDragging = true;
            transform.position = targetPosition;
            timeX += Time.deltaTime;
            timeY += Time.deltaTime;
            // Get the new X and Y positions from the curves
            float newX = movementCurveX.Evaluate(timeX);
            float newY = movementCurveY.Evaluate(timeY);

            // Set the new position of the stirrer
            transform.position = new Vector3(newX, newY, transform.position.z);
            
            // Set isAnimating to true
            isAnimating = true;
            
            // Trigger the onStirrerAnimationStart event
            onStirrerAnimationStart?.Invoke(); // Add this line to trigger the event
            if (isAnimating && Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    // Instantiate the button at the end of the animation
                    InstantiateButton();

                    // Reset the animation flag
                    isAnimating = false;
                }
        }
        else
        {
            // Set isAnimating to false
            isAnimating = false;
        }
        // Check if the animation has ended
        if (!buttonInstantiated && isAnimating && timeX >= movementCurveX.keys[movementCurveX.length - 1].time && timeY >= movementCurveY.keys[movementCurveY.length - 1].time)
            {
                // Instantiate the button at the end of the animation
                InstantiateButton();

                // Set the flag to true to prevent multiple instantiations
                buttonInstantiated = true;

                // Reset the animation flag
                isAnimating = false;
            }
    }

    private IEnumerator StopSecondParticleSystem()
    {
        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        // Stop the second particle system
        secondParticleSystem.Stop();
    }
}