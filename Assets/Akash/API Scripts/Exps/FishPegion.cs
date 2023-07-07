using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FishPegion : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void fpExpSend()
    {
        StartCoroutine(fp());
    }

    IEnumerator fp()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        fpDataSend data = new fpDataSend
        {
            experimentName = "Hibiscus",
            moduleName = "Hibiscus",
            user = Get_Student_Details.guid,
            questions = new List<fpQuestion>()
        };

        fpQuestion fpQuestions = new fpQuestion
        {
            //Questions
            question =
            "",

            //answers
            answer =
            "",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(fpQuestions);

        foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }

        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }

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
public class fpDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<fpQuestion> questions;
    public string marks;
    public List<fpComment> comments;
}

[System.Serializable]
public class fpQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class fpComment
{
    public string name;
    public string comments;
}
