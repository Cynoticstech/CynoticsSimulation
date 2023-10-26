using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;

public class SignUpFB : MonoBehaviour
{
    private void Awake()
    {
        // Initialize the Facebook SDK
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    // This method logs the signup event when a user successfully signs up
    public void SignUpLogFB()
    {
        Debug.Log("assass");
        FB.LogAppEvent("signup", parameters: new Dictionary<string, object> {
            { "status", "success" },
            // You can add more parameters specific to the signup event
        });
    }
}
