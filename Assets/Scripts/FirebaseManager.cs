using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;
    private FirebaseAuth auth;
    public Button yourButton;
    private bool isLoggingIn = false;

    async void Start()
    {
        // Set up the button click listener
        yourButton.onClick.AddListener(OnButtonClick);

        // Initialize Firebase
        await FirebaseApp.CheckAndFixDependenciesAsync();
        
        FirebaseApp app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnButtonClick()
    {
        if (isLoggingIn) return;
        // Check if the user is already logged in
        if (auth.CurrentUser == null)
        {
            isLoggingIn = true;
            // If not logged in, perform anonymous login
            AnonymousLogin();
        }
        else
        {
            // User is already logged in
            Debug.Log("User already logged in with ID: " + auth.CurrentUser.UserId);
        }
    }

    private async void AnonymousLogin()
    {
        try
        {
            AuthResult result = await auth.SignInAnonymouslyAsync();
            FirebaseUser newUser = result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            // Store the UserID in PlayerPrefs
            PlayerPrefs.SetString("UserID", newUser.UserId);
            PlayerPrefs.Save();

            // Save the user ID in the Firebase database
            await SaveUserIdInDatabase(newUser.UserId);
        }
        catch (System.Exception exception)
        {
            Debug.LogError("Anonymous sign in encountered an error: " + exception);
        }
        finally{
            isLoggingIn = false;
        }
    }

    private async Task SaveUserIdInDatabase(string userId)
    {
        User newUser = new User(userId);
        string json = JsonUtility.ToJson(newUser);
        try
        {
            await dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
            Debug.Log("User ID saved successfully.");
        }
        catch (System.Exception exception)
        {
            Debug.LogError("Failed to save user ID: " + exception);
        }
    }

    public void LoadNextSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [System.Serializable]
    public class User
    {
        public string UserID;

        public User(string userId)
        {
            this.UserID = userId;
        }
    }
}