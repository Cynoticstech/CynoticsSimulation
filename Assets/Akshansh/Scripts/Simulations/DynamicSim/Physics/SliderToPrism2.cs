using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderToPrism2 : MonoBehaviour
{
    public Slider slider;
    public GameObject incidentRay;
    public GameObject EmergentPoint;
    public TMP_Text IncDegree;
    public TMP_Text DevDegree;
    public float In_Angle;
    public float Re_Angle;
    float sinI, sinR;
    float RadI, RadR;


    public float minSliderValue = 0.0f;
    public float maxSliderValue = 1.0f;
   

    public Vector2 startPos = new Vector2(1.827f, -0.82f);
    public Vector2 endPos = new Vector2(1.14f, 0.42f);

    private void Start()
    {
        Vector2 initialPosition = Vector2.Lerp(startPos, endPos, Mathf.InverseLerp(minSliderValue, maxSliderValue, slider.value));
        incidentRay.transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, incidentRay.transform.localPosition.z);
    }

    private void Update()
    {
        float normalizedSliderValue = Mathf.InverseLerp(minSliderValue, maxSliderValue, slider.value);
        Vector2 newPosition = Vector2.Lerp(startPos, endPos, normalizedSliderValue);
        incidentRay.transform.localPosition = new Vector3(newPosition.x, newPosition.y, incidentRay.transform.localPosition.z);

        In_Angle = 30 + (slider.value * 30);
        IncDegree.text = In_Angle.ToString() + "<sup>o</sup>";
        sinI = Mathf.Sin(In_Angle);
        sinR = sinI / 1.52f;
        RadR = Mathf.Asin(sinR);
        Re_Angle = RadR * Mathf.Rad2Deg;
        DevDegree.text = (In_Angle + Re_Angle - 60.0f).ToString() + "<sup>o</sup>";
    }
}