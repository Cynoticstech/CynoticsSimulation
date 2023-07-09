using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BioFertilizer : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void BioFExpSend()
    {
        StartCoroutine(BioF());
    }

    IEnumerator BioF()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        BioFDataSend data = new BioFDataSend
        {
            experimentName = "Bio Fertilizers Microbes",
            moduleName = "Bio Fertilizers Microbes",
            user = Get_Student_Details.guid,
            questions = new List<BioFQuestion>()
        };

        BioFQuestion BioFQuestion = new BioFQuestion
        {
            //Questions
            question =
            "A. Azolla\r\n 1. Kingdom\r\n 2. Division\r\n 3. Example\r\n" +
            "B. Anabaena\r\n 1. Kingdom\r\n 2. Division\r\n 3. Example\r\n" +
            "C. Azotobacter\r\n 1. Kingdom\r\n 2. Division\r\n 3. Example\r\n" +
            "D. Nostoc\r\n 1. Kingdom\r\n 2. Division\r\n 3. Example\r\n",

            //answers
            answer =
            "A. Azolla\r\n 1. Plantae\r\n 2. Pteridophyta\r\n 3. Azolla\r\n" +
            "A. Anabaena\r\n 1. Bacteria\r\n 2. Cyanobacteria\r\n 3. Anabaena\r\n" +
            "A. Azotobacter\r\n 1. Bacteria\r\n 2. Proteobacteria\r\n 3. Azotobacter\r\n" +
            "A. Nostoc\r\n 1. Bacteria\r\n 2. Cyanobacteria\r\n 3. Nostoc\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(BioFQuestion);

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
public class BioFDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<BioFQuestion> questions;
    public string marks;
    public List<BioFComment> comments;
}

[System.Serializable]
public class BioFQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class BioFComment
{
    public string name;
    public string comments;
}
