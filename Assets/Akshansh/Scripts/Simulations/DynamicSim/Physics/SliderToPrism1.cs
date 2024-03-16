using UnityEngine;
using UnityEngine.UI;

public class SliderToPrism1 : MonoBehaviour
{
    public Slider slider;
    public GameObject RefractiveRay;
    

    public float minSliderValue = 0.0f;
    public float maxSliderValue = 1.0f;
    public float minRotation = 13.8f;
    public float maxRotation = 35.0f;

    private void Start()
    {
        // Set the initial rotation based on minRotation
        float initialRotation = Mathf.Lerp(minRotation, maxRotation, Mathf.InverseLerp(minSliderValue, maxSliderValue, slider.value));
        RefractiveRay.transform.localRotation = Quaternion.Euler(0f, 0f, initialRotation);
    }

    private void Update()
    {
        float normalizedSliderValue = Mathf.InverseLerp(minSliderValue, maxSliderValue, slider.value);
        float mappedRotation = Mathf.Lerp(minRotation, maxRotation, normalizedSliderValue);

        // Apply the rotation to the GameObject
        RefractiveRay.transform.localRotation = Quaternion.Euler(0f, 0f, mappedRotation);
    }
}
