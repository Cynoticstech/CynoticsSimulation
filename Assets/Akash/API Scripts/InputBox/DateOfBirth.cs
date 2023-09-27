using UnityEngine;
using TMPro;

public class DateOfBirth : MonoBehaviour
{
    public TMP_InputField dayInputField;
    public TMP_InputField monthInputField;
    public TMP_InputField yearInputField;

    private TMP_InputField currentInputField; // Store the currently edited input field

    private void Start()
    {
        // Attach listeners to the input fields' value changed events
        dayInputField.onValueChanged.AddListener(OnDateValueChanged);
        monthInputField.onValueChanged.AddListener(OnDateValueChanged);
        yearInputField.onValueChanged.AddListener(OnDateValueChanged);

        // Initialize the current input field as null
        currentInputField = null;
    }

    private void OnDateValueChanged(string newValue)
    {
        if (currentInputField != null)
        {
            // Ensure that the input is within a valid range (e.g., 1-31 for day)
            int.TryParse(newValue, out int value);
            if (value < 1)
            {
                value = 1;
            }
            else if (value > 31)
            {
                value = 31;
            }

            // Update the currently edited input field with the sanitized value
            currentInputField.text = value.ToString();
        }
    }

    public void SetCurrentInputField(TMP_InputField inputField)
    {
        // Set the currently edited input field
        currentInputField = inputField;
    }
}
