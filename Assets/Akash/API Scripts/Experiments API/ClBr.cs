using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClBr : MonoBehaviour
{
    public SendApiExp sendApi;
    public HalogenUIManager halogenUIManager;
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

        ClBrQuestion lBrQuestion = new ClBrQuestion
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
        data.questions.Add(lBrQuestion);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < halogenUIManager.ApiAnswers.Count; i++)
        {
            sb.Append(halogenUIManager.ApiAnswers[i].text);
            if ((i + 1) % 6 == 0 || i == halogenUIManager.ApiAnswers.Count - 1)
            {
                lBrQuestion.attemptedanswer.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(", ");
            }
        }
        ClBrComment comment = new ClBrComment
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
public class ClBrDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<ClBrQuestion> questions;
    public string marks;
    public ClBrComment comments;
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
