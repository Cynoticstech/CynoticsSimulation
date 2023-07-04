using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Get_User_Data : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;

    private void Start()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return null;

        APIClasses.UserData userData = JsonUtility.FromJson<APIClasses.UserData>(Login_API.userDataJson);
        usernameText.text = userData.username;
        Debug.Log("ok");
    }
}
