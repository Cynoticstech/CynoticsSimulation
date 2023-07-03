using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Andriod_ID : MonoBehaviour
{
    public static string id;
    public void GetAndriodID()
    {
        id = SystemInfo.deviceUniqueIdentifier;
    }
}
