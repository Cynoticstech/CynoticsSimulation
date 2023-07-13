using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Microbes : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    public SendApiExp sendApi;

    public void MicrobeExpSend()
    {
        StartCoroutine(Microbe());
    }

    IEnumerator Microbe()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        MicrobeDataSend data = new MicrobeDataSend
        {
            experimentName = "Industrial Microbes",
            moduleName = "Industrial Microbes",
            user = Get_Student_Details.guid,
            questions = new List<MicrobeQuestion>()
        };

        MicrobeQuestion microbeQuestion = new MicrobeQuestion
        {
            //Questions
            question =
            "Table: \r\n Sr. no, Name of the microbe, Type, Characteristics\r\n" +
            "1, ____, ____, ____ \r\n" +
            "2, ____, ____, ____ \r\n" +
            "3, ____, ____, ____ \r\n" +
            "4, ____, ____, ____ \r\n" +
            "5, ____, ____, ____ \r\n",

            //answers
            answer =
            "",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(microbeQuestion);
        answers = simulationSetupManager.activeSimulation.InputFields;

        foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }

        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }
        MicrobeComment comment = new MicrobeComment
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
public class MicrobeDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<MicrobeQuestion> questions;
    public string marks;
    public MicrobeComment comments;
}

[System.Serializable]
public class MicrobeQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class MicrobeComment
{
    public string name;
    public string comments;
}
