using UnityEngine;
using TMPro;

public class DateOfBirth : MonoBehaviour
{
    public TMP_InputField dobInputField;

    private void Start()
    {
        // Attach a listener to the input field's value changed event
        dobInputField.onValueChanged.AddListener(OnDateValueChanged);
    }

    private void OnDateValueChanged(string newValue)
    {
        // Check if the input length is sufficient to insert the separator
        if (newValue.Length == 2 && !newValue.Contains("/"))
        {
            // Automatically insert the '/' after the day
            dobInputField.text = newValue + "/";
            dobInputField.caretPosition = dobInputField.text.Length;
        }
        else if (newValue.Length == 5 && newValue.Substring(3, 1) != "/")
        {
            // Automatically insert the '/' after the month
            dobInputField.text = newValue.Insert(3, "/");
            dobInputField.caretPosition = dobInputField.text.Length;
        }
    }
}