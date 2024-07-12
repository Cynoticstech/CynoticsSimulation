using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMulitpliedValue : MonoBehaviour
{
    public Slider LeftMassSlider;
    public Slider RightMassSlider;
    public Slider LeftVelocitySlider;
    public Slider RightVelocitySlider;
    public TextMeshProUGUI LeftMomentumValue;
    public TextMeshProUGUI RightMomentumValue;

    private void Start()
    {
        UpdateTextValue();
    }

    public void OnSliderValueChanged()
    {
        UpdateTextValue();
    }

    public void UpdateTextValue()
    {
        float MomentumLeftValue = LeftMassSlider.value * LeftVelocitySlider.value;
        int intValue1 = Mathf.RoundToInt(MomentumLeftValue);
        LeftMomentumValue.text = intValue1.ToString();

        float MomentumRightValue = RightMassSlider.value * RightVelocitySlider.value;
        int intValue2 = Mathf.RoundToInt(MomentumRightValue);
        RightMomentumValue.text = intValue2.ToString();
    }
}
