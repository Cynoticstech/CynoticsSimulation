using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OTP_Verification : MonoBehaviour
{
    [SerializeField] private TMP_InputField firstDigit, secondDigit, thirdDigit, fourthDigit, email;

    [SerializeField] GameObject emailScreen;
    [SerializeField] GameObject newpassScreen;
    IEnumerator SendUserEnteredOtp()
    {
        string _url = "https://echo-admin-backend.vercel.app/api/student/verify-otp";

        APIClasses.OtpSend otpHolder = new APIClasses.OtpSend()
        {
            email = email.text,
            emailOTP = ("" + firstDigit.text + secondDigit.text + thirdDigit.text + fourthDigit.text)
    };

        string jsonBody = JsonUtility.ToJson(otpHolder);
        byte[] rawBody = Encoding.UTF8.GetBytes(jsonBody);

        Debug.Log(jsonBody);
        
        UnityWebRequest request = UnityWebRequest.Post(_url, "application/json");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success In Sending The OTP");
            StartCoroutine(VerifyOTP());
            
        }
        else
        {
            Debug.Log("Error to send OTP");
            Debug.Log(request.error);
        }
        
    }

    IEnumerator VerifyOTP()
    {
        UnityWebRequest newRequest = UnityWebRequest.Get("https://echo-admin-backend.vercel.app/api/student/verify-otp");
        yield return newRequest.SendWebRequest();

        if (newRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Verified");
            SceneManager.LoadScene("Student Login");
            Debug.Log(newRequest.result);
        }
        else
        {
            Debug.Log("Wrong OTP Entered");
            Debug.Log(newRequest.error);
        }
    }

    public void AttemptVerification()
    {
        StartCoroutine(SendUserEnteredOtp());
        Debug.Log("" + firstDigit.text + secondDigit.text + thirdDigit.text + fourthDigit.text);
    }
}
