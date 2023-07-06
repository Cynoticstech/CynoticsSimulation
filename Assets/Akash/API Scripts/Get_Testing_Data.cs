using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Testing_Data : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetProfileData());
    }

    IEnumerator GetProfileData()
    {
        UnityWebRequest newRequest = UnityWebRequest.Get("https://echo-admin-backend.vercel.app/api/student/");
        yield return newRequest.SendWebRequest();

        if (newRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data Retrieved");
            string jsonData = newRequest.downloadHandler.text;
            Debug.Log("JSON Data: " + jsonData);

            ApiResponse data = JsonUtility.FromJson<ApiResponse>(jsonData);
            Debug.Log("Deserialized Data: " + data);

            if (data != null && data.status == "success" && data.user != null)
            {
                string email = data.user.email;
                string instituteId = data.user.instituteId;
                string username = data.user.username;
                string phoneNumber = data.user.phone;
                string dob = data.user.dob;

                Debug.Log("Email: " + email);
                Debug.Log("Institute ID: " + instituteId);
                Debug.Log("Username: " + username);
                Debug.Log("Phone Number: " + phoneNumber);
                Debug.Log("Date of Birth: " + dob);
            }
            else
            {
                Debug.Log("Error: Invalid API response format");
            }
        }
        else
        {
            Debug.Log("Error: Failed to retrieve data");
            Debug.Log(newRequest.error);
        }
    }

    [System.Serializable]
    public class ApiResponse
    {
        public string status;
        public string message;
        public UserData user;
    }

    [System.Serializable]
    public class UserData
    {
        public string image;
        public string guid;
        public string username;
        public string dob;
        public string email;
        public string phone;
        public string instituteId;
        public string @class;
        public string physics;
        public string biology;
        public string chemistry;
        public long registrationDate;
        public string deviceKey;
        public string subsplan;
        public object[] institute;
    }
}

