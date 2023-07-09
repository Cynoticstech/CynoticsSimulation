using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FishPegion : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    SendApiExp sendApi;
    public void fpExpSend()
    {
        StartCoroutine(fp());
    }

    IEnumerator fp()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        fpDataSend data = new fpDataSend
        {
            experimentName = "Chordate animals",
            moduleName = "Chordate animals",
            user = Get_Student_Details.guid,
            questions = new List<fpQuestion>()
        };

        fpQuestion fpQuestions = new fpQuestion
        {
            //Questions
            question =
            "Parts of fish: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n Part 8\r\n Part 9\r\n" +
            "Observation:\r\n A. Fish\r\n Kingdom \r\n Phylum \r\n Sub-phylum \r\n Class \r\n Example - Fish \r\n" +
            "Parts of pegion: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n" +
            "Observation:\r\n A. Pegion\r\n Kingdom \r\n Phylum \r\n Sub-phylum \r\n Class \r\n Example - Pegion \r\n" +
            "Characteristics:\r\n 1. Pigeon’s body is covered with ____. Forelimbs are modified into ____. Heart has ____ compartments.\r\n 2. Pigeon has ____ bones and air bags inside the body. This is an aerial adaptation.\r\n",

            //answers
            answer =
            "Parts of fish: \r\n Spiny dorsal fin\r\n eye\r\n mouth\r\n gills\r\n pelvic fin\r\n pectoral fin\r\n anal fin\r\n caudal fin\r\n soft dorsal fin\r\n" +
            "Observation:\r\n A. Fish\r\n Animalia \r\n Chordata \r\n Vertebrata \r\n Pisces \r\n Example - Fish \r\n" +
            "Parts of pegion: \r\n Beak\r\n eye\r\n neck\r\n contour feathers\r\n wing feathers\r\n calws\r\n digits\r\n" +
            "Observation:\r\n A. Pegion\r\n Animalia \r\n Chordata \r\n Vertebrata \r\n Aves \r\n Example - Pegion \r\n" +
            "Characteristics:\r\n 1. feathers \r\n 2. wings \r\n 3. four \r\n 4. hollow ",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(fpQuestions);
        answers = simulationSetupManager.activeSimulation.InputFields;

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
public class fpDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<fpQuestion> questions;
    public string marks;
    public List<fpComment> comments;
}

[System.Serializable]
public class fpQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class fpComment
{
    public string name;
    public string comments;
}
