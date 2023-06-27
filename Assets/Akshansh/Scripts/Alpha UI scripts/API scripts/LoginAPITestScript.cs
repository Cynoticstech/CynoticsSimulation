using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginAPITestScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {


        WWWForm form = new WWWForm();
        /* string data = "{\"fullName\": \"My test fullName\", \"dob\": \"1682761673617\", " +
             "\"email\": \"Mytest@test.com\", \"phone\": \"1234567890\",\"InstituteId\": \"Mytest-employee-id\"}";*/

        //Debug.Log(data);
        data myData = new data();
        myData.fullName = "MyName";
        myData.dob = "1025";
        myData.email = "MyMail.c";
        myData.phone = "10254785";
        myData.InstituteId = "EmplyID";

        string dataToSend = JsonUtility.ToJson(myData);
        
        form.AddField("myField", "myData");
        //object jsonObj = JsonUtility.FromJson<object>(data);

        using (UnityWebRequest www = UnityWebRequest.Post
            ("https://echo-admin-backend.vercel.app/api/student/signup", dataToSend))

        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(dataToSend);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
class data
{
    public string fullName, dob, email, phone, InstituteId;
}
class CredentialData
{
    public string superAdminAccessKeyc, password;
}

class DeleteUserClass
{
    public string _id;
}
