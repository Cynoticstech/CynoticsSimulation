using UnityEngine;
using TMPro;

public class OTPInputHandler : MonoBehaviour
{
    public TMP_InputField[] otpInputFields;
    private int currentIndex = 0;

    private void Start()
    {
        // Set up initial selection
        SelectInputField(currentIndex);
    }

    private void Update()
    {
        // Handle Backspace input
        if (Input.GetKeyDown(KeyCode.Backspace) && currentIndex > 0)
        {
            currentIndex--;
            SelectInputField(currentIndex);
        }

        // Handle digit input
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Keypad0 + i))
            {
                if (currentIndex < otpInputFields.Length)
                {
                    otpInputFields[currentIndex].text = i.ToString();
                    currentIndex++;

                    if (currentIndex < otpInputFields.Length)
                    {
                        SelectInputField(currentIndex);
                    }
                    else
                    {
                        currentIndex = otpInputFields.Length - 1;
                    }
                }
            }
        }
    }

    private void SelectInputField(int index)
    {
        // Deselect all fields
        foreach (TMP_InputField inputField in otpInputFields)
        {
            inputField.DeactivateInputField();
        }

        // Select the specified field and move the cursor to the end
        otpInputFields[index].ActivateInputField();
        otpInputFields[index].MoveTextEnd(false);
    }
}