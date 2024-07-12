using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PipetteController : MonoBehaviour
{
    public GameObject pipetteSprite;
    public Button pipetteButton;
    private bool hasPipetted = false;
    public Vector3 beakerPosition = new Vector3(-3.0f, 1.5f, 0);  // Example coordinates
    public Vector3 flaskPosition = new Vector3(2.0f, -1.0f, 0);   // Example coordinates
    private Quaternion originalRotation;
    public LineRenderer lineRenderer;
    public Transform pipetteOpening;
    public Transform flaskTarget;
    public SpriteRenderer flaskSpriteRenderer;
    public AnimationCurve waterLevelCurve;
    public SpriteRenderer beakerSpriteRenderer;  // SpriteRenderer for the beaker
    public AnimationCurve beakerWaterLevelCurve; // AnimationCurve for the beaker
    public AudioSource rotationSound;


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;   
        pipetteButton.onClick.AddListener(OnPipetteButtonClick);
        originalRotation = pipetteSprite.transform.rotation;
        lineRenderer.enabled = false;  // Initially disable the LineRenderer
    }

    void OnPipetteButtonClick()
    {
        if (!hasPipetted)
        {
            StartCoroutine(MovePipette());
            hasPipetted = true;
            //pipetteButton.interactable = false;
        }
        else
        {
            Debug.Log("Attempting to show toast");
            ShowToast("Already pipetted out 10 ml succinic acid solution");
        }
    }

    IEnumerator MovePipette()
    {
        Vector3 originalPosition = pipetteSprite.transform.position;

        // Move to beaker position
        yield return MoveToPosition(beakerPosition, 1f);
        yield return RotatePipette(75f, 1f);  // Rotate by 90 degrees
        yield return StartCoroutine(DecreaseBeakerWaterLevel(2f));  // Decrease water level in the beaker

        yield return RotatePipette(0f, 1f);  // Rotate back to the original position

        // Move to flask position
        yield return MoveToPosition(flaskPosition, 1f);
        yield return RotatePipette(75f, 1f);  // Rotate by 90 degrees
        StartLineRenderer();  // Start the line renderer to simulate liquid pouring
        yield return StartCoroutine(FillFlask(2f));
        StopLineRenderer();  // Stop the line renderer
        yield return RotatePipette(0f, 1f);  // Rotate back to the original position

        // Move back to original position
        yield return MoveToPosition(originalPosition, 1f);
    }

    IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 start = pipetteSprite.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            pipetteSprite.transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        pipetteSprite.transform.position = target;
    }

    IEnumerator RotatePipette(float targetAngle, float duration)
    {
        Quaternion startRotation = pipetteSprite.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            pipetteSprite.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        pipetteSprite.transform.rotation = targetRotation;
    }

    IEnumerator DecreaseBeakerWaterLevel(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float fillAmount = beakerWaterLevelCurve.Evaluate(elapsed / duration);
            beakerSpriteRenderer.material.SetFloat("_WaterLevel", fillAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }
        beakerSpriteRenderer.material.SetFloat("_WaterLevel", beakerWaterLevelCurve.Evaluate(1f));
    }

    private void StartLineRenderer()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, pipetteOpening.position);
        lineRenderer.SetPosition(1, flaskTarget.position);
    }

    private void StopLineRenderer()
    {
        lineRenderer.enabled = false;
    }
    IEnumerator FillFlask(float duration)
    {
        rotationSound.Play();
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float fillAmount = waterLevelCurve.Evaluate(elapsed / duration);
            flaskSpriteRenderer.material.SetFloat("_WaterLevel", fillAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }
        flaskSpriteRenderer.material.SetFloat("_WaterLevel", waterLevelCurve.Evaluate(1f));
        rotationSound.Stop();
    }

    private void ShowToast(string message)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", context, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
                toastObject.Call("show");
                Debug.Log("Toast shown: " + message);
            }));
        }
        #else
        Debug.Log("Toast: " + message);
        #endif
    }
}
