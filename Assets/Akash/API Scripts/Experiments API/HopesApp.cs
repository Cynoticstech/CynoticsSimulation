using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HopesApp : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;
    public string dummyData;

    public void Hopes()
    {
        StartCoroutine(h());
    }

    IEnumerator h()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        hDataSend data = new hDataSend
        {
            experimentName = "Hope's apparatus",
            moduleName = "Hope's apparatus",
            user = Get_Student_Details.guid,
            questions = new List<hQuestion>()
        };

        hQuestion hque = new hQuestion
        {
            //Questions
            question =
            "Observation table:\r\n No, Time(mins), Temp(T1)<sup>O</sup>C, Temp(T2)<sup>O</sup>C\r\n" +
            "1, ____, ____, ____\r\n" +
            "2, ____, ____, ____\r\n" +
            "3, ____, ____, ____\r\n" +
            "4, ____, ____, ____\r\n" +
            "5, ____, ____, ____\r\n" +
            "6, ____, ____, ____\r\n" +
            "7, ____, ____, ____\r\n" +
            "8, ____, ____, ____\r\n" +
            "9, ____, ____, ____\r\n" +
            "10, ____, ____, ____\r\n" +
            "Inference: \r\n 1. The water ___________ as the temperature in the inner cylinder _____________ due to freezing mixture and density of water increases. Therefore the temperature reading in both the thermometers are ____________ .\r\n 2. If temperature decreases below _______________<sup>O</sup>C water starts expanding and water becomes lighter due to decrease in density. It moves upward in the cylinder and the temperature of the upper part of the water column decreases to 0<sup>O</sup>C.\r\n 3. The behaviour of water between the temperature 0<sup>O</sup>C to 4<sup>O</sup>C is known as __________\r\n 4. What conclusion can be drawn from the graph? How is anomalous behaviour of water proved?" +
            "Options:\r\n A. The graph shows that water expands when cooled, unlike other substances.\r\nB. The graph indicates that water boils at a lower temperature at higher pressures.\r\nC. The graph demonstrates that water freezes at a higher temperature in the presence of impurities.\r\nD. The graph illustrates that water evaporates more quickly at higher temperatures.\r\n",
  
            //answers
            answer =
           "Observation table:\r\n No, Time(mins), Temp(T1)<sup>O</sup>C, Temp(T2)<sup>O</sup>C\r\n" +
            "1, 0, 12, 12\r\n" +
            "2, 1, 8.2, 11.8\r\n" +
            "3, 2, 6, 11\r\n" +
            "4, 3, 5, 10\r\n" +
            "5, 4, 4.5, 8.2\r\n" +
            "6, 5, 4, 5.8\r\n" +
            "7, 6, 4, 1.4\r\n" +
            "8, 7, 3.5, 0\r\n" +
            "9, 8, 1.8, 0\r\n" +
            "10, 9, 0, 0\r\n" +
            "Inference:\r\n 1.contracts \r\n 2. decreases/lowers\r\n 3. same\r\n 4. 4<sup>0</sup>C \r\n 5. anomalous behaviour of water \r\n 6. Option: A\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(hque);
        dummyData = "1, 0, 12, 12\r\n" +
            "2, 1, 8.2, 11.8\r\n" +
            "3, 2, 6, 11\r\n" +
            "4, 3, 5, 10\r\n" +
            "5, 4, 4.5, 8.2\r\n" +
            "6, 5, 4, 5.8\r\n" +
            "7, 6, 4, 1.4\r\n" +
            "8, 7, 3.5, 0\r\n" +
            "9, 8, 1.8, 0\r\n" +
            "10, 9, 0, 0\r\n";
        hque.attemptedanswer.Add(dummyData);

        foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }

        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }
        hComment comment = new hComment
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
public class hDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<hQuestion> questions;
    public string marks;
    public hComment comments;
}

[System.Serializable]
public class hQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class hComment
{
    public string name;
    public string comments;
}
