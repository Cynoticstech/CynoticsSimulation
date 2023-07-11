using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AceticAcid : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;

    public void ActExpSend()
    {
        StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        ActDataSend data = new ActDataSend
        {
            experimentName = "Acetic Acid",
            moduleName = "Odour \r\n Solubility in water \r\n Effect of litmus \r\n Lime Water \r\n",
            user = Get_Student_Details.guid,
            questions = new List<ActQuestion>()
        };

        ActQuestion ACTQuestion = new ActQuestion
        {
            //Questions
            question = "",

            //answers
            answer = "",


            //attempted answers
            attemptedanswer = new List<string>()
        };

        data.questions.Add(ACTQuestion);

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
public class ActDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<ActQuestion> questions;
    public string marks;
    public List<ActComment> comments;
}

[System.Serializable]
public class ActQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class ActComment
{
    public string name;
    public string comments;
}

