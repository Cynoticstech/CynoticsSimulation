using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Cooling : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;
    public string dummyData;

    public void cool()
    {
        StartCoroutine(cooli());
    }

    IEnumerator cooli()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        cooliDataSend data = new cooliDataSend
        {
            experimentName = "Temperature of hot water during natural cooling",
            moduleName = "Temperature of hot water during natural cooling",
            user = Get_Student_Details.guid,
            questions = new List<cooliQuestion>()
        };

        cooliQuestion cooliQuestion = new cooliQuestion
        {
            //Questions
            question =
            "Observation:\r\n 1. Initial temperature of the water before heating = ____<sup>oC</sup>\r\n" +
            "Observation table: \r\n Time(mins), Temp<sup>O</sup>C\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "Inference: \r\n 1. The rate of cooling of water is ____ when the difference in the temperature of water and the ambience is large.\r\n 2. This rate ____ as the temperature of water reduces due to cooling.",

            //answers
            answer =
            "Observation:\r\n 1. 27<sup>OC</sup>\r\n" +
            "Observation table: \r\n Time(mins), Temp<sup>O</sup>C\r\n" +
            "0, 70\r\n" +
            "1, 60\r\n" +
            "2, 48\r\n" +
            "3, 41\r\n" +
            "4, 36\r\n" +
            "5, 33\r\n" +
            "Inference: \r\n 1. higher \r\n 2. decreases/reduces\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(cooliQuestion);

        dummyData =
            "0, 70\r\n" +
            "1, 60\r\n" +
            "2, 48\r\n" +
            "3, 41\r\n" +
            "4, 36\r\n" +
            "5, 33\r\n";
        cooliQuestion.attemptedanswer.Add(dummyData);
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
public class cooliDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<cooliQuestion> questions;
    public string marks;
    public List<cooliComment> comments;
}

[System.Serializable]
public class cooliQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class cooliComment
{
    public string name;
    public string comments;
}
