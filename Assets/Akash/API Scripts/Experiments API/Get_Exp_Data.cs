using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Exp_Data : MonoBehaviour
{
    public APIClasses.DataSend[] test;

    private void Start()
    {
        StartCoroutine(GetUserExperiment());    
    }

    IEnumerator GetUserExperiment()
    {
        string userGuid = Get_Student_Details.guid;
        string baseUrl = "https://echo-admin-backend.vercel.app/api/experiments/";
        string url = baseUrl + userGuid;

        UnityWebRequest newRequest = UnityWebRequest.Get(url);
        yield return newRequest.SendWebRequest();

        var temp = JsonUtility.FromJson<APIClasses.DataSend[]>(newRequest.downloadHandler.text);
        test = temp;
       
        Debug.Log(newRequest.downloadHandler.text);

        if (newRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Started1");
        }
        else
        {
            Debug.Log(newRequest.error);
        }
    }
}
