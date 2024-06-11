using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI LeftMassText;
    public TextMeshProUGUI RightMassText;
    public TextMeshProUGUI LeftVelocityText;
    public TextMeshProUGUI RightVelocityText;
    public Slider LeftMassSlider;
    public Slider RightMassSlider;
    public Slider LeftVelocitySlider;
    public Slider RightVelocitySlider;
    public Slider slider; // Reference to the Slider component
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the text based on the slider's initial value
        UpdateText(slider.value);

        // Add a listener to the slider to call UpdateText whenever its value changes
        slider.onValueChanged.AddListener(delegate { UpdateText(slider.value); });
    }

    public void UpdateTextValue()
    {
        int LeftMassValue = Mathf.RoundToInt(LeftMassSlider.value);
        LeftMassText.text = LeftMassValue.ToString();

        int RightMassValue = Mathf.RoundToInt(RightMassSlider.value);
        RightMassText.text = RightMassValue.ToString();

        int LeftVelocityValue = Mathf.RoundToInt(LeftVelocitySlider.value);
        LeftVelocityText.text = LeftVelocityValue.ToString();
        
        int RightVelocityValue = Mathf.RoundToInt(RightVelocitySlider.value);
        RightVelocityText.text = RightVelocityValue.ToString();
    }

    // Update the text to display the current value of the slider
    void UpdateText(float value)
    {
        // Update the TextMeshPro text based on the slider value
        if (value == 0)
        {
            textMeshPro.text = "Elastic collision";
        }
        else if (value == 1)
        {
            textMeshPro.text = "Inelastic collision";
        }
    }
}
