using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent_AndroidId : MonoBehaviour
{
    public static string UsableID;

    void Awake()
    {
        UsableID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log(UsableID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
