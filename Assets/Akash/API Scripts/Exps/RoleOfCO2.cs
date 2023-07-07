using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RoleOfCO2 : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void CO2ExpSend()
    {
        StartCoroutine(CO2());
    }

    IEnumerator CO2()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        CO2DataSend data = new CO2DataSend
        {
            experimentName = "Role of Carbon Dioxide",
            moduleName = "Role of Carbon Dioxide",
            user = Get_Student_Details.guid,
            questions = new List<CO2Question>()
        };

        CO2Question co2Questions = new CO2Question
        {
            //Questions
            question =
            "Observation:\r\n 1. Carbon Dioxide evolved in the respiration is absorbed by ____ therefore water level in the bent tube rises.\r\n" +
            "Inference:\r\n 1. ____ gas evolved during respiration in plants.\r\n",
           
            //answers
            answer =
            "Observation:\r\n 1. Potassium hydroxide/KOH\r\n" +
            "Inference:\r\n Carbon dioxide/CO2\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(co2Questions);

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
public class CO2DataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<CO2Question> questions;
    public string marks;
    public List<CO2Comment> comments;
}

[System.Serializable]
public class CO2Question
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class CO2Comment
{
    public string name;
    public string comments;
}