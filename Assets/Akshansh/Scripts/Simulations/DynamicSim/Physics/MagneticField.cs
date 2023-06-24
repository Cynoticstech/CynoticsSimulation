using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagneticField : MonoBehaviour
{
    [SerializeField] Slider ohmsSlider;
    [SerializeField] TMP_Text ohmsText;
    [SerializeField] string currentText;
    [SerializeField] int curtSliderIndex;
    [SerializeField] SpriteRenderer switchObj;
    [SerializeField] GameObject offField,onField;
    [SerializeField] GameObject[] fieldLevels;

    private void Start()
    {
        ohmsSlider.onValueChanged.AddListener((val) => SetSliderValue((int)val));
    }
    void SetSliderValue(int _value)
    {
        curtSliderIndex = _value;
        ohmsText.text = currentText + (_value+1);
    }
    public void ToggleSwitch()
    {
        switchObj.enabled =!switchObj.enabled;
        offField.SetActive(!switchObj.enabled);
        onField.SetActive(switchObj.enabled);
    }

    public void Shake()
    {
        if (!switchObj.enabled)
            return;
        foreach(var v in fieldLevels)
        {
            v.SetActive(false);
        }
        fieldLevels[curtSliderIndex].SetActive(true);
    }
}
