using Simulations;
using Simulations.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Cockroach : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    public SendApiExp sendApi;
    public void cockroachExpSend()
    {
        StartCoroutine(cockroachie());
    }

    IEnumerator cockroachie()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        CockroachDataSend data = new CockroachDataSend
        {
            experimentName = "Non-Chordate animals",
            moduleName = "Non-Chordate animals",
            user = Get_Student_Details.guid,
            questions = new List<CockroachQuestion>()
        };

        CockroachQuestion cockroachQuestions = new CockroachQuestion
        {
            //Questions
            question =
            "Cockroach: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n Part 8\r\n Part 9\r\n Part 10\r\n Part 11\r\n Part 12\r\n Part 13\r\n" +
            "Observation: A. Cockroach\r\n Kingdom \r\n Sub-kingdom \r\n Phylum \r\n Class \r\n Example - Cockroach \r\n" +
            "Earthworm: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n" +
            "Observation: A. Earthworm\r\n Kingdom \r\n Sub-kingdom \r\n Phylum \r\n Example - Earthworm \r\n",

            //answers
            answer =
            "Cockroach: \r\n Metathorzx \r\n Mesothorax \r\n Prothorax\r\n Foreleg\r\n Head\r\n Antenna\r\n Compound eye\r\n Wing\r\n Segmented Abdomen\r\n Stylus\r\n Cercus\r\n Hindleg\r\n Midleg\r\n" +
            "Observation: A. Cockroach\r\n Animalia \r\n Non-chordata \r\n Arthropoda \r\n Insecta \r\n Example - Cockroach \r\n" +
            "Earthworm: \r\n Mouth \r\n Anus \r\n Segments \r\n Clitellum \r\n" +
            "Observation: A. Earthworm\r\n Animalia \r\n Non-chordata \r\n Annelida \r\n Example - Earthworm \r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(cockroachQuestions);

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
public class CockroachDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<CockroachQuestion> questions;
    public string marks;
    public List<CockroachComment> comments;
}

[System.Serializable]
public class CockroachQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class CockroachComment
{
    public string name;
    public string comments;
}
