using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ReactivityOfMetals : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void ReactivityOfMetalsExpSend()
    {
        StartCoroutine(ReactivityOfMetal());
    }

    IEnumerator ReactivityOfMetal()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        ReactivityOfMetalsDataSend data = new ReactivityOfMetalsDataSend
        {
            experimentName = "Reactivity of metals",
            moduleName = "Reactivity of metals",
            user = Get_Student_Details.guid,
            questions = new List<ReactivityOfMetalsQuestion>()
        };

        ReactivityOfMetalsQuestion ReactQuestion = new ReactivityOfMetalsQuestion
        {
            //Questions
            question =
            "Observation Table: \r\n Metal, Al<sub>2</sub>(SO<sub>4</sub>)<sub>3</sub>, ZnSO<sub>4</sub>, FeSO<sub>4</sub>, CuSO<sub>4</sub>\r\n" +
            "Aluminium, ____, Zinc is displaced, Iron is displaced, copper is displaced \r\n" +
            "Zinc, No reaction, ____, Iron is displaced, copper is displaced \r\n" +
            "Iron, No reaction, No reaction, ____, copper is displaced \r\n" +
            "Copper, No reaction, No reaction, No reaction, ____ \r\n" +
            "Inference\r\n The decreasing order of reactivity of the metal is: ____ > ____ > ____ > ____\r\n",

            //answers
            answer =
             "Observation Table: \r\n Metal, Al<sub>2</sub>(SO<sub>4</sub>)<sub>3</sub>, ZnSO<sub>4</sub>, FeSO<sub>4</sub>, CuSO<sub>4</sub>\r\n" +
            "Aluminium, No reaction, Zinc is displaced, Iron is displaced, copper is displaced \r\n" +
            "Zinc, No reaction, No reaction, Iron is displaced, copper is displaced \r\n" +
            "Iron, No reaction, No reaction, No reaction, copper is displaced \r\n" +
            "Copper, No reaction, No reaction, No reaction, No reaction \r\n" +
            "Inference\r\n The decreasing order of reactivity of the metal is: Al > Zn > Fe > Cu\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(ReactQuestion);

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
public class ReactivityOfMetalsDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<ReactivityOfMetalsQuestion> questions;
    public string marks;
    public List<ReactivityOfMetalsComment> comments;
}

[System.Serializable]
public class ReactivityOfMetalsQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class ReactivityOfMetalsComment
{
    public string name;
    public string comments;
}
