using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class API1sttest : MonoBehaviour
{

    void Start()
    {
        //StartCoroutine(TestingStuRegistration());
        StartCoroutine(TestingStuRegistration11());
    }

    void Update()
    {
        
    }

    IEnumerator TestingStuRegistration11()
    {
        Debug.Log("STARTED");
        string _url = "https://echo-admin-backend.vercel.app/api/student/";

        data myData = new data()
        {
            email = "Roy11@gmail.com",
            password = "akash",
            phone = "1234567890",
            instituteId = "roy_ID",
            username = "Roy",
            dob = "11-8-96",
        };

        string jsonBody = JsonUtility.ToJson(myData);
        byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

        Debug.Log(jsonBody);
        UnityWebRequest request = UnityWebRequest.Get(_url);

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            Debug.Log(request.downloadHandler.text);

        }
        else
        {
            Debug.Log("error");
            Debug.Log(request.error);
        }
    }

    IEnumerator TestingStuRegistration()
    {
        Debug.Log("STARTED");
        string _url = "https://echo-admin-backend.vercel.app/api/student/signup";

        data myData = new data()
        {
            email = "Roy11@gmail.com",
            password = "akash",
            phone = "1234567890",
            instituteId = "roy_ID",
            username = "Roy",
            dob = "11-8-96",
        };

        string jsonBody = JsonUtility.ToJson(myData);
        byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

        Debug.Log(jsonBody);
        UnityWebRequest request = UnityWebRequest.Post(_url, "application/json");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            Debug.Log(request.downloadHandler.data);
        }
        else
        {
            Debug.Log("error");
            Debug.Log(request.error);
        }
    }
}
