using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPassword : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    public Button showPasswordButton;

    private bool isPasswordVisible = false;
    private TMP_InputField.ContentType originalContentType;

    private void Start()
    {
        originalContentType = passwordInputField.contentType;
        showPasswordButton.onClick.AddListener(TogglePasswordVisibility);
    }

    public void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;
        passwordInputField.contentType = isPasswordVisible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        passwordInputField.ForceLabelUpdate();
        //showPasswordButton.GetComponentInChildren<Text>().text = isPasswordVisible ? "Hide Password" : "Show Password";
    }
}
