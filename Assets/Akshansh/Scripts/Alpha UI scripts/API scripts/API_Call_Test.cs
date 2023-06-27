using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class API_Call_Test : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(SendRequest());
    }
    /*private string GetAuthenticationKey()
    {
        //throw new NotImplementedException();
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    void SignUp()
    {
        //StartCoroutine(SendRequest());
    }


    [System.Obsolete]
    IEnumerator SendRequest()
    {
            string requestURL = "https://cynotics-backend-web-service.onrender.com/api/user/signup";
            print("Called");
            data myData = new data()
            {
                username = "Roy",
                dob = "11-8-96",
                email = "Roy@gmail.com",
                instituteId = "roy_ID",
                phone = "1234567890"

            };

            // convert object to JSON
            string jsonBody = JsonUtility.ToJson(myData);

            // convert JSON to raw bytes
            byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

            UnityWebRequest request = new UnityWebRequest(requestURL, "POST");
            request.uploadHandler = new UploadHandlerRaw(rawBody);
            request.downloadHandler = new DownloadHandlerBuffer();
            //request.SetRequestHeader("Authorization", "Basic " + GetAuthenticationKey());
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                // error
                //request.error
                Debug.Log(request.error);
                // request.responseCode
            }

            else
            {
                // successful
                Debug.Log("Complete");
            }
        
    }
}