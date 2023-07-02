using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class Login_API : MonoBehaviour
{
    [SerializeField] private TMP_InputField email, password;

    IEnumerator Login()
    {
        string _url = "https://echo-admin-backend.vercel.app/api/student/login";

        APIClasses.LoginDataHolder loginData = new APIClasses.LoginDataHolder()
        {
            email = email.text,
            password = password.text,
            deviceKey = "111008"
        };

        string jsonBody = JsonUtility.ToJson(loginData);
        byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

        Debug.Log(jsonBody);

        UnityWebRequest request = UnityWebRequest.Post(_url, "application/json");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data Sent");
            StartCoroutine(GetLoginCreds());
        }
        else
        {
            Debug.Log("Error to send data");
            Debug.Log(request.error);
        }
    }

    IEnumerator GetLoginCreds()
    {
        UnityWebRequest newRequest = UnityWebRequest.Get("https://echo-admin-backend.vercel.app/api/student/");
        yield return newRequest.SendWebRequest();

        if (newRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data Retrived");
            SceneManager.LoadScene("Main Alpha Functionality Pages");
            Debug.Log(newRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error to retrive data");
            Debug.Log(newRequest.error);
        }
    }

    public void AttemptLogin()
    {
        StartCoroutine(Login());
    }
}