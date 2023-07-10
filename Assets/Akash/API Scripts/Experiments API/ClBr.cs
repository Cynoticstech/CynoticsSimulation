using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ClBr : MonoBehaviour
{
    public TMP_InputField[] answers;
    public TMP_Dropdown dropdown1, dropdown2, dropdown3, dropdown4, dropdown5, dropdown6;
    public string d1, d2, d3, d4, d5, d6, d7, d8;

    public void ClBrExpSend()
    {
        StartCoroutine(Cl());
    }
    public void AddDataOfCLBR()
    {
        d1 = dropdown1.options[dropdown1.value].text;
        d2 = dropdown2.options[dropdown2.value].text;
        d3 = dropdown3.options[dropdown3.value].text;
        d4 = dropdown4.options[dropdown4.value].text;
        d5 = dropdown5.options[dropdown5.value].text;
        d6 = dropdown6.options[dropdown6.value].text;
        d7 = "Chmeical Reaction:" + d1 + "+" + d2 + "---->" + d3 + "+" + d4 + "Colour of precipitate:" + d5 + "Ion:" + d6 + "\r\n";

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
        lBrQuestion.attemptedanswer.Add(d7);
        data.questions.Add(lBrQuestion);

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
