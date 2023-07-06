using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Student_Details : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    public TMP_InputField emailInputField;
    public TMP_InputField usernamez;

    public static string email;
    public static string instituteId;
    public static string username;
    public static string phoneNumber;
    public static string dob;
    public static string guid;

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

            // Deserialize the JSON response
            APIClasses.ApiResponse data = JsonUtility.FromJson<APIClasses.ApiResponse>(jsonData);
            Debug.Log("Deserialized Data: " + data);

            if (data != null && data.status == "success" && data != null)
            {
                email = data.user.email;
                instituteId = data.user.instituteId;
                username = data.user.username;
                phoneNumber = data.user.phone;
                dob = data.user.dob;
                guid = data.user.guid;

                foreach (TMP_InputField inputField in inputFields)
                {
                    string placeholderText = inputField.placeholder.GetComponent<TextMeshProUGUI>().text;
                    Debug.Log(placeholderText);
                }

                inputFields[0].text = username;
                inputFields[1].text = email;
                inputFields[2].text = dob;
                inputFields[3].text = phoneNumber;
                inputFields[4].text = instituteId;

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

    IEnumerator UpdateUserDetails(string field, string value)
    {
        APIClasses.UserUpdateData updateData = new APIClasses.UserUpdateData();
        updateData.field = field; updateData.value = value;

        string jsonData = JsonUtility.ToJson(updateData);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = UnityWebRequest.Put("https://echo-admin-backend.vercel.app/api/student/", bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data Updated Successfully");
        }
        else
        {
            Debug.Log("Error: Failed to update data");
            Debug.Log(request.error);
        }
    }
    public void SaveChanges()
    {
        /*string newEmail = emailInputField.text;
        StartCoroutine(UpdateUserDetails("email", newEmail));*/

        //string newUserName = username.text;
        //StartCoroutine(UpdateUserDetails("username", newUserName));
    }

}
