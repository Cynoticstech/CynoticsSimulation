using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuretteLevelMovement : MonoBehaviour
{
    public Button invisibleButton; // Assign your invisible button in the inspector
    public GameObject openingPoint; // Assign the opening point game object in the inspector
    public GameObject closingPoint; // Assign the closing point game object in the inspector
    public LineRenderer lineRenderer; // Assign the LineRenderer in the inspector
    public float speed = 1f; // Speed of the water falling
    public Camera secondaryCamera; // Assign your secondary camera in the inspector
    public float cameraSpeed = 0.1f; // Speed of the camera movement

    public AnimationCurve buretteCurve; // Assign the burette animation curve in the inspector
    public AnimationCurve flaskCurve; // Assign the flask animation curve in the inspector
    public SpriteRenderer buretteRenderer; // Assign the burette sprite renderer in the inspector
    public SpriteRenderer flaskRenderer; // Assign the flask sprite renderer in the inspector

    private bool isWaterFalling = false; // Flag to control the water falling
    private Coroutine buretteCoroutine;
    private Coroutine flaskCoroutine;
    private Coroutine colorCoroutine;
    private float animationTime = 0f; // Current time in the animation curves

    private Color originalFlaskColor; // Store the original color of the flask's liquid
    public Color lightPinkColor = new Color(1f, 0.75f, 0.8f); // The color to change to when the value is above 1.51
    public Color darkPinkColor = new Color(1f, 0.41f, 0.71f); // The color to change to when the value is below 1.50

    private bool shouldStayPink = false; // Flag to check if the color should stay pink
    private bool shouldTurnDarkPink = false; // Flag to check if the color should turn dark pink
    public AudioSource waterSound; // Assign the water sound in the inspector
    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, openingPoint.transform.position);
        lineRenderer.SetPosition(1, openingPoint.transform.position);
        lineRenderer.enabled = false; // Initially disable the LineRenderer

        // Setup EventTrigger for invisibleButton
        EventTrigger trigger = invisibleButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(pointerUpEntry);

        // Get the original color of the flask's liquid
        originalFlaskColor = flaskRenderer.material.GetColor("_Color01");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartWaterFall();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopWaterFall();
    }

    void StartWaterFall()
    {
        if (!isWaterFalling)
        {
            isWaterFalling = true;
            lineRenderer.enabled = true; // Enable the LineRenderer when the button is pressed
            waterSound.Play(); // Play the water sound
            StartCoroutine(WaterFall());
            StartCoroutine(MoveCameraDown()); // Start moving the camera down

            // Start coroutines for water levels
            buretteCoroutine = StartCoroutine(UpdateBuretteWaterLevel());
            flaskCoroutine = StartCoroutine(UpdateFlaskWaterLevel());

            // Start the coroutine to change the flask's color
            if (colorCoroutine != null)
            {
                StopCoroutine(colorCoroutine);
            }
            colorCoroutine = StartCoroutine(ChangeFlaskColor(lightPinkColor));
        }
    }

    void StopWaterFall()
    {
        if (isWaterFalling)
        {
            isWaterFalling = false;
            lineRenderer.enabled = false; // Disable the LineRenderer when the button is released
            waterSound.Stop(); // Stop the water sound
            // Stop coroutines for water levels
            if (buretteCoroutine != null)
            {
                StopCoroutine(buretteCoroutine);
            }
            if (flaskCoroutine != null)
            {
                StopCoroutine(flaskCoroutine);
            }

            // Start the coroutine to revert the flask's color after one second if it should not stay pink or turn dark pink
            if (colorCoroutine != null)
            {
                StopCoroutine(colorCoroutine);
            }
            if (!shouldStayPink && !shouldTurnDarkPink)
            {
                colorCoroutine = StartCoroutine(RevertFlaskColorAfterDelay(1f));
            }
        }
    }

    IEnumerator UpdateBuretteWaterLevel()
    {
        while (isWaterFalling)
        {
            float buretteLevel = buretteCurve.Evaluate(animationTime);
            buretteRenderer.material.SetFloat("_WaterLevel", buretteLevel);

            // Check if the burette level crosses the thresholds
            if (buretteLevel < 1.49f)
            {
                shouldTurnDarkPink = true;
                shouldStayPink = false;
                if (colorCoroutine != null)
                {
                    StopCoroutine(colorCoroutine);
                }
                colorCoroutine = StartCoroutine(ChangeFlaskColor(darkPinkColor));
                Debug.Log("Burette level below 1.49f, turning dark pink.");
            }
            else if (buretteLevel >= 1.50f && buretteLevel <= 1.51f)
            {
                shouldStayPink = true;
                shouldTurnDarkPink = false;
                if (colorCoroutine != null)
                {
                    StopCoroutine(colorCoroutine);
                }
                colorCoroutine = StartCoroutine(ChangeFlaskColor(lightPinkColor));
                Debug.Log("Burette level between 1.50f and 1.51f, staying pink.");
            }

            else if (buretteLevel > 1.51f && buretteLevel <= 1.84f)
            {
                // Color should change to pink and in one second revert back to original color
                shouldStayPink = false;
                shouldTurnDarkPink = false;
                if (colorCoroutine != null)
                {
                    StopCoroutine(colorCoroutine);
                }
                colorCoroutine = StartCoroutine(ChangeFlaskColorAndRevert(lightPinkColor, 1f));
                Debug.Log("Burette level between 1.51f and 1.84f, color should change to pink and revert after 1s.");
            }

            else if (buretteLevel > 1.84f)
            {
                // Color should not change
                shouldStayPink = false;
                shouldTurnDarkPink = false;
                if (colorCoroutine != null)
                {
                    StopCoroutine(colorCoroutine);
                }
                colorCoroutine = StartCoroutine(RevertFlaskColorAfterDelay(0f));
                Debug.Log("Burette level above 1.84f, color should not change.");
            }

            animationTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator UpdateFlaskWaterLevel()
    {
        while (isWaterFalling)
        {
            float flaskLevel = flaskCurve.Evaluate(animationTime);
            flaskRenderer.material.SetFloat("_WaterLevel", flaskLevel);
            animationTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator WaterFall()
    {
        while (isWaterFalling && Vector3.Distance(lineRenderer.GetPosition(1), closingPoint.transform.position) > 0.01f)
        {
            lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), closingPoint.transform.position, speed * Time.deltaTime));
            yield return null;
        }
    }

    IEnumerator MoveCameraDown()
    {
        while (isWaterFalling)
        {
            secondaryCamera.transform.position -= new Vector3(0, cameraSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    IEnumerator ChangeFlaskColor(Color newColor)
    {
        flaskRenderer.material.SetColor("_Color01", newColor);
        yield return null;
    }

    IEnumerator ChangeFlaskColorAndRevert(Color newColor, float delay)
    {
        flaskRenderer.material.SetColor("_Color01", newColor);
        yield return new WaitForSeconds(delay);
        if (!shouldStayPink && !shouldTurnDarkPink)
        {
            flaskRenderer.material.SetColor("_Color01", originalFlaskColor);
        }
    }

    IEnumerator RevertFlaskColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!shouldStayPink && !shouldTurnDarkPink)
        {
            flaskRenderer.material.SetColor("_Color01", originalFlaskColor);
        }
    }
}
