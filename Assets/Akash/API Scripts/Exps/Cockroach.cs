using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Cockroach : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void cockroachExpSend()
    {
        StartCoroutine(cockroachie());
    }

    IEnumerator cockroachie()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        CockroachDataSend data = new CockroachDataSend
        {
            experimentName = "Cockroach",
            moduleName = "Cockroach",
            user = Get_Student_Details.guid,
            questions = new List<CockroachQuestion>()
        };

        CockroachQuestion cockroachQuestions = new CockroachQuestion
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
        data.questions.Add(cockroachQuestions);

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
public class CockroachDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<CockroachQuestion> questions;
    public string marks;
    public List<CockroachComment> comments;
}

[System.Serializable]
public class CockroachQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class CockroachComment
{
    public string name;
    public string comments;
}
