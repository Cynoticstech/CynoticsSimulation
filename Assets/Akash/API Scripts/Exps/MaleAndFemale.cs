using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MaleAndFemale : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void MFExpSend()
    {
        StartCoroutine(MF());
    }

    IEnumerator MF()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        MFDataSend data = new MFDataSend
        {
            experimentName = "Male and Female",
            moduleName = "Male and Female",
            user = Get_Student_Details.guid,
            questions = new List<MFQuestion>()
        };

        MFQuestion mfQuestions = new MFQuestion
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
        data.questions.Add(mfQuestions);

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
public class MFDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<MFQuestion> questions;
    public string marks;
    public List<MFComment> comments;
}

[System.Serializable]
public class MFQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class MFComment
{
    public string name;
    public string comments;
}