using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MitosisandMeiosis : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void MitosisMeosisExpSend()
    {
        StartCoroutine(MitosisMeosis());
    }

    IEnumerator MitosisMeosis()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        MitosisDataSend data = new MitosisDataSend
        {
            experimentName = "Mitosis and meiosis",
            moduleName = "Mitosis and meiosis",
            user = Get_Student_Details.guid,
            questions = new List<MitosisQuestion>()
        };

        MitosisQuestion mitosisQuestion = new MitosisQuestion
        {
            //Questions
            question = "Stages of mitosis:\r\n Stage 1\r\n Stage 2\r\n Stage 3\r\n Stage 4\r\n Stage 5\r\n Stage 6\r\n Stage 7\r\n"+
            "Stages of meiosis:\r\n Stage 1\r\n Stage 2\r\n Stage 3\r\n Stage 4\r\n Stage 5\r\n Stage 6\r\n Stage 7\r\n Stage 8\r\n Stage 9\r\n Stage 10\r\n"+
            "Inference:\r\n Due to mitosis, the number of chromosomes in the cell _____ therefore, this division occurs in ____ cells.\r\n"+
            "Due to meiosis, the number of chromosomes in the cell ____  so this division occurs in ____ cells.\r\n",

            //answers
            answer = "Stages of mitosis:\r\nInterphase\r\nProphase\r\nLate Prophaser\nMetaphase\r\nAnaphase\r\nTelophase\r\nCytokinesis\r\n" +
            "Stages of meiosis:\r\nInterphase\r\nProphase I\r\nMetaphase I\r\nAnaphase I\r\nTelophase 1\r\nInterkinesis\r\nProphase II\r\nMetaphase II\r\nAnaphase II\r\nTelophase II\r\n" +
            "Inference:\r\nremains the same\r\nsomatic\r\nreduces by half\r\ngerm\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(mitosisQuestion);

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
public class MitosisDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<MitosisQuestion> questions;
    public string marks;
    public List<MitosisComment> comments;
}

[System.Serializable]
public class MitosisQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class MitosisComment
{
    public string name;
    public string comments;
}