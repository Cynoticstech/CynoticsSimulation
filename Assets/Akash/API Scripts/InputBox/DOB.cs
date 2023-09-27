using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DOB : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        // Get a reference to the TMP Input Field component.
        inputField = GetComponent<TMP_InputField>();

        // Subscribe to the OnValueChanged event to detect changes in the input field.
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string newText)
    {
        // Check if the input length is greater than 2 and the last character is a digit.
        if (newText.Length > 2 && char.IsDigit(newText[newText.Length - 1]))
        {
            // Insert a slash '/' after the second character.
            newText = newText.Insert(2, "/");

            // Set the input field's text to the modified text.
            inputField.text = newText;
        }
    }
}

