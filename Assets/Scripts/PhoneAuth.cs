using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Database;

public class User
{
    public long PhoneNumber;
    public bool IsPaidUser;

    public User(long phoneNumber)
    {
        PhoneNumber = phoneNumber;
        IsPaidUser = false; // Assuming default value
    }
}

public class PhoneAuth : MonoBehaviour
{
    public TMP_InputField phoneInp, EnterCode_Inp;
    public GameObject AllOptions;
    uint phoneAuthTimeoutMs = 3 * 60000;
    FirebaseAuth auth;
    PhoneAuthProvider provider;
    DatabaseReference reference;
    private string UserId;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        UserId = SystemInfo.deviceUniqueIdentifier;
    }

    public void sendSMS()
    {
        #if UNITY_EDITOR
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
          },
          verificationFailed: (error) =>
          {
              print("Error: " + error);
              
          },
          codeSent: (id, token) =>
          {
              
              PlayerPrefs.SetString("verificationId", id);
              
              ShowToast("Verifying OTP… Please wait");
          },
          codeAutoRetrievalTimeOut: (id) =>
          {
              
          });
          #endif
    }

    public void VerifyOtp()
    {
        #if UNITY_EDITOR
        
        MockSubmitOtp();
    #else
        print("verify otp pressed");
        string verificationId = PlayerPrefs.GetString("verificationId");
        string verificationCode = EnterCode_Inp.text;

        
        ShowToast("OTP verification in progress…");

        PhoneAuthCredential credential = provider.GetCredential(verificationId, verificationCode);

        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " +
                               task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("User signed in successfully");
            
            Debug.Log("Phone number: " + newUser.PhoneNumber);
            
            Debug.Log("Phone provider ID: " + newUser.ProviderId);

            AllOptions.SetActive(false);
            SceneManager.LoadScene("Main Alpha Functionality Pages");
        });
        #endif
    }
    public void CreateUser(){
        User newUser = new User(long.Parse(phoneInp.text));
        string json = JsonUtility.ToJson(newUser);
        reference.Child("users").Child(UserId).SetRawJsonValueAsync(json).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("User created successfully");
                PlayerPrefs.SetString("UserId", UserId);
                PlayerPrefs.Save();
            }
        });  
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
    
    print("Verification completed (mock)");
    
}
#endif
#if UNITY_EDITOR
private void MockSubmitOtp() {
    
    print("OTP submitted (mock)");
    
    SceneManager.LoadScene("Main Alpha Functionality Pages");
}
#endif
}