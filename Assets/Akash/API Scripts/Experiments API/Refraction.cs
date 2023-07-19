using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Refraction : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;

    public void Prism()
    {
        StartCoroutine(GlassPrismm());
    }
    IEnumerator GlassPrismm()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        RefractionDataSend data = new RefractionDataSend
        {
            experimentName = "Laws Of Refration Of Light",
            moduleName = "Laws Of Refration Of Light",
            user = Get_Student_Details.guid,
            questions = new List<RefractionQuestion>()
        };

        RefractionQuestion reaf = new RefractionQuestion
        {
            //Questions
            question =
            "Observation Table: \r\n No., Angle of Incidence(i), Angle of refraction(r), Angle of deviation(d)\r\n" +
            "1, 30, ____, ____" +
            "2, 45, ____, ____" +
            "3, 60, ____, ____" +
            "Inference:\r\n 1. When light undergoes refraction through a glass slab, the ____ and ____ are parallel to each other.\r\n 2. The angle of incidence and the angle of emergence are of ____ measures.\r\n",

            //answers
            answer =
            "Observation Table: \r\n No., Angle of Incidence(i), Angle of refraction(r), Angle of deviation(d)\r\n" +
            "1, 30, ____, 53.27" +
            "2, 45, ____, 39.26" +
            "3, 60, ____, 40.45" +
            "Inference:\r\n 1. Incident\r\n 2. Emergent\r\n 3. Equal",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(reaf);
        foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }
        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }
        RefractionComment comment = new RefractionComment
        {
            name = "",
            comments = ""
        };

        data.comments = comment;
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = UnityWebRequest.Put(apiUrl, "application/json");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Experiments data sent successfully!");
            sendApi.SuccessAPISentPopup();
        }
        else
        {
            Debug.Log("Failed to send experiments data. Error: " + request.error);
            sendApi.UnsuccessAPISentPopup();
        }
    }
}

[System.Serializable]
public class RefractionDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<RefractionQuestion> questions;
    public string marks;
    public RefractionComment comments;
}

[System.Serializable]
public class RefractionQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class RefractionComment
{
    public string name;
    public string comments;
}