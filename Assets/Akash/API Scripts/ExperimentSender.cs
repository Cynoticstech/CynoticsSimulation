using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ExperimentSender : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(SendExperimentsData());
    }

    public void expSend()
    {
        StartCoroutine(SendExperimentsData());
    }

    IEnumerator SendExperimentsData()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        ExperimentsData data = new ExperimentsData
        {
            experimentName = "",
            moduleName = "",
            user = Get_Student_Details.guid,
            questions = new List<Question>
        {
            new Question
            {
                question = "Vha se htoo ,()",
                answer = "",
                attemptedanswer = new List<string>
                {
                    "25",
                    "123"
                }
            }
        },
            marks = "",
            comments = new List<Comment>
        {
            new Comment
            {
                name = "John",
                comments = ""
            }
        }
        };

        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = UnityWebRequest.Post(apiUrl, "application/json");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Experiments data sent successfully!");
        }
        else
        {
            Debug.Log("Failed to send experiments data. Error: " + request.error);
        }
    }

}

[System.Serializable]
public class ExperimentsData
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<Question> questions;
    public string marks;
    public List<Comment> comments;
}

[System.Serializable]
public class Question
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class Comment
{
    public string name;
    public string comments;
}
