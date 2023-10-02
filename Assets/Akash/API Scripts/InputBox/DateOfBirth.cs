/*using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DateOfBirth : MonoBehaviour
{
    public TMP_InputField tmpInputField;

    private string previousText = "";

    private void Awake()
    {
        tmpInputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public void OnValueChanged()
    {
        string input = tmpInputField.text;

        // Remove any non-digit and non-slash characters
        input = Regex.Replace(input, @"[^\d/]", "");

        // Insert slashes at appropriate positions for date format
        if (input.Length > 2 && input[2] != '/')
        {
            input = input.Insert(2, "/");
        }
        if (input.Length > 5 && input[5] != '/')
        {
            input = input.Insert(5, "/");
        }

        // Use a Coroutine to update text and cursor position after a slight delay
        StartCoroutine(UpdateInputField(input));
    }

    private System.Collections.IEnumerator UpdateInputField(string input)
    {
        // Delay for one frame
        yield return null;

        // Update the text field and reset the cursor position
        tmpInputField.SetTextWithoutNotify(input);
        tmpInputField.caretPosition = input.Length;
        previousText = input;
    }
}
*/

using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DateOfBirth : MonoBehaviour
{
    public TMP_InputField tmpInputField;

    private string previousText = "";

    private void Awake()
    {
        tmpInputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public void OnValueChanged()
    {
        string input = tmpInputField.text;

        // Remove any non-digit and non-slash characters
        input = Regex.Replace(input, @"[^\d/]", "");

        // Check if the last character typed is a digit
        if (input.Length > 0 && char.IsDigit(input[input.Length - 1]))
        {
            // Check if we need to insert a slash after day (dd) and month (mm)
            if (input.Length == 2 && input[1] != '/')
            {
                // Insert slash after day
                input = input.Insert(2, "/");
            }
            else if (input.Length == 5 && input[4] != '/')
            {
                // Insert slash after month
                input = input.Insert(5, "/");
            }
        }
        else if (input.EndsWith("/"))
        {
            // Handle backspacing after a slash by removing it
            input = input.Substring(0, input.Length - 1);
        }

        // Use a Coroutine to update text and cursor position after a slight delay
        StartCoroutine(UpdateInputField(input));
    }

    private System.Collections.IEnumerator UpdateInputField(string input)
    {
        // Delay for one frame
        yield return null;

        // Update the text field and reset the cursor position
        tmpInputField.SetTextWithoutNotify(input);
        tmpInputField.caretPosition = input.Length;
        previousText = input;
    }
}











