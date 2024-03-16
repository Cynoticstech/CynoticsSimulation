using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Andriod_ID : MonoBehaviour
{
    public GameObject popup;
    public GameObject mainPage;
    public Button acceptButton;
    public Button denyButton;
    public static string deviceId;

    private const string PermissionRequestedKey = "PermissionRequested";

    private void Start()
    {
        if (PlayerPrefs.HasKey(PermissionRequestedKey))
        {
            if (PlayerPrefs.GetInt(PermissionRequestedKey) == 1)
            {
                GenerateDeviceID();
                Debug.Log(deviceId);
                mainPage.SetActive(true);
                popup.SetActive(false);
                return;
            }
        }

        popup.SetActive(true);
        acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        denyButton.onClick.AddListener(OnDenyButtonClicked);
    }

    private void OnAcceptButtonClicked()
    {
        GenerateDeviceID();
        PlayerPrefs.SetInt(PermissionRequestedKey, 1);
        PlayerPrefs.Save();
        mainPage.SetActive(true);
        popup.SetActive(false);
    }

    private void OnDenyButtonClicked()
    {
        Application.Quit();
    }

    private void GenerateDeviceID()
    {
        deviceId = SystemInfo.deviceUniqueIdentifier;
    }
}
