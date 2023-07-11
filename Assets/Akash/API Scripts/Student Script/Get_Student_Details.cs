using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Student_Details : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public TextMeshProUGUI[] instituteText;

    public SpriteRenderer instituteImageRenderer;

    //User
    public static string image, guid, username, dob, email, phone, instituteId, @class, physics, biology, chemistry, registrationDate, deviceKey, subsplan;

    //Institute
    public static string _id, instituteGuid, displayId, createdAt, InstitutedisplayId, Institutename, InstituteregistrationDate, lastTransactionDate, paymentStatus,
        numberOfKeys, numberOfStudents, numberOfTeachers, pincode, teachersInfo, adminusername, adminpancard, adminaadharcard, adminemail, adminphone, emailOtpInfo,
        phoneOtpInfo, isEmailVerified, isPhoneVerified, isLoginActive, type, password, Instituteimage, slogan, organizationName, address, Instituteemail, Institutephone;

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
            APIClasses.ApiResponse data = JsonUtility.FromJson<APIClasses.ApiResponse>(jsonData);

            // Access user data
            if (data != null && data.status == "success" && data.user != null)
            {
                image = data.user.image;
                guid = data.user.guid;
                username = data.user.username;
                dob = data.user.dob;
                email = data.user.email;
                phone = data.user.phone;
                instituteId = data.user.instituteId;
                @class = data.user.@class;
                physics = data.user.physics; biology = data.user.biology; chemistry = data.user.chemistry;
                registrationDate = data.user.registrationDate;
                deviceKey = data.user.deviceKey; subsplan = data.user.subsplan;

                _id = data.user.institute._id; instituteGuid = data.user.institute.guid; displayId = data.user.institute.displayId;
                createdAt = data.user.institute.createdAt; InstitutedisplayId = data.user.institute.displayId; Institutename = data.user.institute.name;
                InstituteregistrationDate = data.user.institute.registrationDate; lastTransactionDate = data.user.institute.lastTransactionDate;
                paymentStatus = data.user.institute.paymentStatus; numberOfKeys = data.user.institute.numberOfKeys; numberOfStudents = data.user.institute.numberOfStudents;
                numberOfTeachers = data.user.institute.numberOfTeachers; pincode = data.user.institute.pincode; teachersInfo = data.user.institute.teachersInfo;
                adminusername = data.user.institute.adminusername; adminpancard = data.user.institute.adminpancard; adminaadharcard = data.user.institute.adminaadharcard;
                adminemail = data.user.institute.adminemail; adminphone = data.user.institute.adminphone; emailOtpInfo = data.user.institute.emailOtpInfo; phoneOtpInfo = data.user.institute.phoneOtpInfo;
                isEmailVerified = data.user.institute.isEmailVerified; isPhoneVerified = data.user.institute.isPhoneVerified; isLoginActive = data.user.institute.isLoginActive;
                type = data.user.institute.type; password = data.user.institute.password; Instituteimage = data.user.institute.image; slogan = data.user.institute.slogan;
                organizationName = data.user.institute.organizationName; address = data.user.institute.address; Instituteemail = data.user.institute.email; Institutephone = data.user.institute.phone;

                inputFields[0].text = username; inputFields[1].text = email; inputFields[2].text = dob; inputFields[3].text = phone; inputFields[4].text = instituteId;
                instituteText[0].text = Institutename; instituteText[1].text = slogan;
                LoadInstImage();

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

    public void LoadInstImage()
    {
        StartCoroutine(LoadImageFromURL(Instituteimage));
    }

    IEnumerator LoadImageFromURL(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            instituteImageRenderer.sprite = sprite;
        }
        else
        {
            Debug.Log("Error loading image: " + request.error);
        }
    }
}