using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class PhoneAuth : MonoBehaviour
{
    public TMP_InputField phoneInp, EnterCode_Inp;
    //public TextMeshProUGUI logTxt;
    public GameObject AllOptions; //all options
    //public GameObject SuccessUi;
    uint phoneAuthTimeoutMs = 3 * 60000; //minutes to milisec
    FirebaseAuth auth;
    PhoneAuthProvider provider;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        provider = PhoneAuthProvider.GetInstance(auth);
         // Check if a user is already signed in
        if (FirebaseAuth.DefaultInstance.CurrentUser != null) {
            // A user is already signed in
            Debug.Log("User is already signed in");
            SceneManager.LoadScene("Main Alpha Functionality Pages");
        } else {
            // No user is signed in
            Debug.Log("No user is signed in");
            // Show the sign-in UI
        }
    }

    public void sendSMS()
    {
        #if UNITY_EDITOR
        // Use a mock authentication system in the Unity editor
        MockVerifyPhoneNumber();
        #else
        ShowToast("Sending OTP… Please wait");
        print("send sms pressed");

        string userNumber = "+91" + phoneInp.text;

        PhoneAuthProvider provider = PhoneAuthProvider.GetInstance(auth);
        provider.VerifyPhoneNumber(
          new PhoneAuthOptions
          {
              PhoneNumber = userNumber,
              TimeoutInMilliseconds = 60000,
              ForceResendingToken = null
          },
          verificationCompleted: (credential) =>
          {
              print("Verification completed");
              //showLogMsg("Verification completed");

              // Auto-sms-retrieval or instant validation has succeeded (Android only).
              // There is no need to input the verification code.
              // `credential` can be used instead of calling GetCredential().
          },
          verificationFailed: (error) =>
          {
              print("Error: " + error);
              //showLogMsg(error);

              // The verification code was not sent.
              // `error` contains a human readable explanation of the problem.
          },
          codeSent: (id, token) =>
          {
              //showLogMsg("Code send success!!");
              // Verification code was successfully sent via SMS.
              // `id` contains the verification id that will need to be passed in with
              PlayerPrefs.SetString("verificationId", id);
              // the code from the user when calling GetCredential().
              // `token` can be used if the user requests the code be sent again, to
              // tie the two requests together.

              // Show toast message
              ShowToast("Verifying OTP… Please wait");
          },
          codeAutoRetrievalTimeOut: (id) =>
          {
              // Called when the auto-sms-retrieval has timed out, based on the given
              // timeout parameter.
              // `id` contains the verification id of the request that timed out.
          });
          #endif
    }

    public void VerifyOtp()
    {
        #if UNITY_EDITOR
        // Use a mock authentication system in the Unity editor
        MockSubmitOtp();
    #else
        print("verify otp pressed");
        string verificationId = PlayerPrefs.GetString("verificationId");
        string verificationCode = EnterCode_Inp.text;

        // Show toast message
        ShowToast("OTP verification in progress…");

        PhoneAuthCredential credential = provider.GetCredential(verificationId, verificationCode);

        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " +
                               task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("User signed in successfully");
            //showLogMsg("Success");
            // This should display the phone number.
            Debug.Log("Phone number: " + newUser.PhoneNumber);
            // The phone number providerID is 'phone'.
            Debug.Log("Phone provider ID: " + newUser.ProviderId);

            AllOptions.SetActive(false);
            SceneManager.LoadScene("Main Alpha Functionality Pages");
        });
        #endif
    }

    private void ShowToast(string message)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", context, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
                toastObject.Call("show");
            }));
        }
        #else
        Debug.Log("Toast: " + message);
        #endif
    }
    #if UNITY_EDITOR
private void MockVerifyPhoneNumber() {
    // Simulate the behavior of Firebase Phone Authentication
    print("Verification completed (mock)");
    //showLogMsg("Verification completed (mock)");
}
#endif
#if UNITY_EDITOR
private void MockSubmitOtp() {
    // Simulate the behavior of Firebase Phone Authentication
    print("OTP submitted (mock)");
    //showLogMsg("OTP submitted (mock)");
    // Simulate successful sign-in
    SceneManager.LoadScene("Main Alpha Functionality Pages");
}
#endif
}