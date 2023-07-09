using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClBr : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void ClBrExpSend()
    {
        StartCoroutine(Cl());
    }

    IEnumerator Cl()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        ClBrDataSend data = new ClBrDataSend
        {
            experimentName = "To identify chloride, bromide and iodide",
            moduleName = "To identify chloride, bromide and iodide",
            user = Get_Student_Details.guid,
            questions = new List<ClBrQuestion>()
        };

        ClBrQuestion ClBrQuestion = new ClBrQuestion
        {
            //Questions
            question =
            "Observation Table: \r\n Test tube, Chemical reaction, Colour of precipitate, Ion\r\n" +
            "A, ____, ____, ____ \r\n" +
            "B, ____, ____, ____ \r\n" +
            "C, ____, ____, ____ \r\n",
            
            //answers
            answer =
            "Observation Table: \r\n Test tube, Chemical reaction, Colour of precipitate, Ion\r\n" +
            "A, KCl + AgNO<sub>3</sub> ---- >AgCl ↓ + KNO<sub>3</sub>, White, Cl<sup>-</sup> \r\n" +
            "B, KBr + AgNO<sub>3</sub> ----> AgBr ↓ + KNO<sub>3</sub>, light yellow, Br<sup>-</sup> \r\n" +
            "C, Kl + AgNO<sub>3</sub> ----> AgI ↓ + KNO<sub>3</sub>, bright yellow, I<sup>-</sup> \r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(ClBrQuestion);

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
public class ClBrDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<ClBrQuestion> questions;
    public string marks;
    public List<ClBrComment> comments;
}

[System.Serializable]
public class ClBrQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class ClBrComment
{
    public string name;
    public string comments;
}
