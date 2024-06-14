using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DraggableWatchGlass : MonoBehaviour
{
    private Vector3 offset;
    public GameObject buttonPrefab; // Assign your button prefab in the Inspector
    private bool isDragging = false;
    private bool hasCollided = false; // Add this variable at the top of your script
    private Vector3 targetPosition = new Vector3(-7.16f, -0.87f, 0f);
    private float moveSpeed = 10f; // Adjust this speed as needed
    private float proximityThreshold = 1f; // Distance threshold for automatic movement

    public TextMeshProUGUI textMeshPro; // Reference to your TextMeshPro component
    public string newTextAfterCollision = "50.591 g"; // Text after collision
    public string originalText;
    public string newTextAfterCollisionwithReset = "0.591 mg"; // Text after collision
    private bool resetButtonClicked = false; // Flag to track reset button click
    private string alphaFunctionalityPagesSceneName = "Main Alpha Functionality Pages";

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        // Store the initial text value
        originalText = textMeshPro.text;
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

    private void Update()
    {
        // Check if the Back button was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.orientation = ScreenOrientation.Portrait;
            // If the Back button is pressed, load the Alpha Functionality Pages scene
            SceneManager.LoadScene(alphaFunctionalityPagesSceneName);
        }
        
        if (isDragging)
        {
            // Check if the object is close enough to the target position
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget < proximityThreshold)
            {
                // Smoothly move the object toward the target position
                float t = Mathf.Clamp01(distanceToTarget / proximityThreshold); // Normalize the distance
                transform.position = Vector3.Lerp(transform.position, targetPosition, t * moveSpeed * Time.deltaTime);

                // Display the "50.000 gm" text
                textMeshPro.text = "50.000 gm";
            }

            else
            {
                // Revert to the original text when watch glass is removed from the target position
                textMeshPro.text = originalText;
            }
        }
    }
    public void ResetText()
    {
        // Called when the reset button is clicked
        isDragging = false;
        resetButtonClicked = true;
        textMeshPro.text = "0.000 mg"; // Set to 0.000 mg
    }
    private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the other collider is the Sprite
    if (other.CompareTag("SpriteTag")) // Replace "SpriteTag" with your actual tag
        {
            // Update the TextMeshPro text
            isDragging = false;
            if (resetButtonClicked)
            {
                resetButtonClicked = false;
                textMeshPro.text = newTextAfterCollisionwithReset; // Set to 0.591 mg
            }
            else
            {
                textMeshPro.text = newTextAfterCollision; // Set to 50.591 g
            }
            if (!hasCollided)
            {
                InstantiateButton();
                hasCollided = true;
            }
        }
    }
    public void OnButtonClick()
    {
        PlayerPrefs.SetString("previousScene", "Scene1");
        SceneManager.LoadScene("Scene2"); // Replace "NewScene" with the name of your scene
    }
    private void InstantiateButton()
    {
        Vector3 worldPosition = new Vector3(7.797f, 2.901f, 2.23f);
        GameObject button = Instantiate(buttonPrefab);

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

        button.GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    public void OnButtonClick2()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        // Load the Momentum scene when the button is clicked
        SceneManager.LoadScene(alphaFunctionalityPagesSceneName);
    }
}