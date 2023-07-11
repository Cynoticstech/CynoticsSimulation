using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Student_Details : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    public TMP_InputField emailInputField;
    public TMP_InputField usernamez;

    // User
    public static string email;
    public static string instituteId;
    public static string username;
    public static string phoneNumber;
    public static string dob;
    public static string guid;

    // Institute
    public static string Id;
    public static string instituteName;
    public static string instituteGuid;

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
            //Debug.Log("JSON Data: " + jsonData);

            // Deserialize the JSON response
            APIClasses.ApiResponse data = JsonUtility.FromJson<APIClasses.ApiResponse>(jsonData);
            //Debug.Log("Deserialized Data: " + data);

            // Access user data
            if (data != null && data.status == "success" && data.user != null)
            {
                email = data.user.email;
                instituteId = data.user.instituteId;
                username = data.user.username;
                phoneNumber = data.user.phone;
                dob = data.user.dob;
                guid = data.user.guid;

                // Access institute data
                if (data.user.institute != null && data.user.institute.Count > 0)
                {
                    APIClasses.InstituteData institute = data.user.institute[0];
                    Id = institute.displayId;
                    instituteName = institute.name;
                    instituteGuid = institute.guid;
                }

                foreach (TMP_InputField inputField in inputFields)
                {
                    string placeholderText = inputField.placeholder.GetComponent<TextMeshProUGUI>().text;
                }

                inputFields[0].text = username;
                inputFields[1].text = email;
                inputFields[2].text = dob;
                inputFields[3].text = phoneNumber;
                inputFields[4].text = instituteId;

                Debug.Log(instituteId);
                Debug.Log(instituteName);
                Debug.Log(instituteGuid);
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
}





/*using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Student_Details : MonoBehaviour
{
    public TMP_InputField[] inputFields;

    public TMP_InputField emailInputField;
    public TMP_InputField usernamez;

    //User
    public static string email;
    public static string instituteId;
    public static string username;
    public static string phoneNumber;
    public static string dob;
    public static string guid;

    //Institute
    public static string _id, displayId, Iname;
    public static string instituteGuid;

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
            //Debug.Log("JSON Data: " + jsonData);

            // Deserialize the JSON response
            APIClasses.ApiResponse data = JsonUtility.FromJson<APIClasses.ApiResponse>(jsonData);
            //Debug.Log("Deserialized Data: " + data);

            // Access user data
            if (data != null && data.status == "success" && data != null)
            {
                email = data.user.email;
                instituteId = data.user.instituteId;
                username = data.user.username;
                phoneNumber = data.user.phone;
                dob = data.user.dob;
                guid = data.user.guid;

                // Access institute data
                List<APIClasses.InstituteData> institutes = data.user.institute;
                if (institutes != null && institutes.Count > 0)
                {
                    APIClasses.InstituteData Institute = institutes[0];
                    _id = Institute._id;
                    displayId = Institute.displayId;
                    name = Institute.name;
                    instituteGuid = Institute.guid;
                }

                foreach (TMP_InputField inputField in inputFields)
                {
                    string placeholderText = inputField.placeholder.GetComponent<TextMeshProUGUI>().text;
                }

                inputFields[0].text = username;
                inputFields[1].text = email;
                inputFields[2].text = dob;
                inputFields[3].text = phoneNumber;
                inputFields[4].text = instituteId;

                Debug.Log(_id);
                Debug.Log(name);
                Debug.Log(displayId);
                Debug.Log(guid);
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
}

*/