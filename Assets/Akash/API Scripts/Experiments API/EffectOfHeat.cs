using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EffectOfHeat : MonoBehaviour
{
    public TMP_InputField answers;
    public SendApiExp sendApi;
    public IceManager ceManager;
    public void EOH()
    {
        StartCoroutine(EOHi());
    }

    IEnumerator EOHi()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        EOHDataSend data = new EOHDataSend
        {
            experimentName = "Effect of heat on ice\r\n",
            moduleName = "Effect of heat on ice\r\n",
            user = Get_Student_Details.guid,
            questions = new List<EOHQuestion>()
        };

        EOHQuestion EOHQuestion = new EOHQuestion
        {
            //Questions
            question =
            "Observation:\r\n 1. Least count of thermometer = ____<sup>O</sup>C\r\n" +
            "Observation table: \r\n Min time, Temp<sup>O</sup>C\r\n" +
            "0,0" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "____, ____\r\n" +
            "Inference: \r\n 1. Heat energy is absorbed during the transformation of ice in to water and water into water vapours\r\n",

            //answers
            answer =
            "0,0\r\n" +
            "1, 0\r\n" +
            "2, 10\r\n" +
            "3, 21\r\n" +
            "4, 30\r\n" +
            "5, 42\r\n" +
            "6, 56\r\n" +
            "7, 72\r\n" +
            "8, 81\r\n" +
            "9, 93\r\n" +
            "Inference: \r\n 1. Heat energy is absorbed during the transformation of ice in to water and water into water vapours\r\n",

            //attempted answers
            attemptedanswer = new List<string>()

        };
        data.questions.Add(EOHQuestion);
        EOHQuestion.attemptedanswer.Add(answers.text);

        foreach( var table in ceManager.ApiAnswerList )
        {
            EOHQuestion.attemptedanswer.Add(table.transform.GetChild(0).GetComponent<TMP_Text>().text+ "," +table.transform.GetChild(1).GetComponent<TMP_Text>().text);
        }

        /*foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }

        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }*/
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
public class EOHDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<EOHQuestion> questions;
    public string marks;
    public List<EOHComment> comments;
}

[System.Serializable]
public class EOHQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class EOHComment
{
    public string name;
    public string comments;
}
