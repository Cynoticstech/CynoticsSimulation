using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBook : MonoBehaviour
{
    void Awake()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("Is Initialized");
        }
        else
        {
            //Handle FB.Init
            FB.Init(() => {
                FB.ActivateApp();
                Debug.Log("Is Initialized Second");
            });
        }
    }
}
