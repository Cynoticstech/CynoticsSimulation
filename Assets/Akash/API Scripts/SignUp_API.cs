using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SignUp_API : MonoBehaviour
{
    //SignUp Fields
    [SerializeField] private TMP_InputField email, password, phone, instituteId, username, dob, confirmPassword;

    //Pannels
    [SerializeField] private GameObject signUpPannel, phoneOtp, emailOtp;

    IEnumerator Signup()
    {
        Debug.Log("Signup Started");
        string _url = "https://echo-admin-backend.vercel.app/api/student/signup";

        APIClasses.SignUpDataHolder signUpData = new APIClasses.SignUpDataHolder()
        {
            email = email.text,
            password = password.text,
            phone = phone.text,
            instituteId = instituteId.text,
            username = username.text,
            dob = dob.text
        };

        string jsonBody = JsonUtility.ToJson(signUpData);
        byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

        Debug.Log(jsonBody);
        UnityWebRequest request = UnityWebRequest.Post(_url, "application/json");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success");
            Debug.Log(request.downloadHandler.data);
        }
        else
        {
            Debug.Log("error"); //Popup for errorw
            Debug.Log(request.error);
        }
    }

    public void AttemptSignUP()
    {
        if(email.text == string.Empty || password.text == string.Empty || phone.text == string.Empty || instituteId.text == string.Empty || username.text == string.Empty || dob.text == string.Empty)
        {
            Debug.Log("Fill all fields to continue"); //ShowPopup for fill all fields
            signUpPannel.SetActive(true);
            return; 
        }

        if(password.text != confirmPassword.text)
        {
            Debug.Log("Passsword Mismatched"); //Popup for password mismatch
            signUpPannel.SetActive(true);
            return;
        }

        StartCoroutine(Signup());
        emailOtp.SetActive(true);
    }
}
