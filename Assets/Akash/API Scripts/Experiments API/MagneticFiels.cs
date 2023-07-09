using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MagneticFiels : MonoBehaviour
{
    public TMP_InputField[] answers;

    public void MagneticFields()
    {
        StartCoroutine(Mag());
    }

    IEnumerator Mag()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        MagDataSend data = new MagDataSend
        {
            experimentName = "Magnetic field due to electric current\r\n",
            moduleName = "Magnetic field due to electric current\r\n",
            user = Get_Student_Details.guid,
            questions = new List<MagQuestion>()
        };

        MagQuestion MagQuestion = new MagQuestion
        {
            //Questions
            question =
            "Observation:\r\n 1. When electric current starts flowing through the coil, ____ are produced at each point on the coil.\r\n 2. As we go away from the wire, the concentric circles representing magnetic lines of force will become ____ \r\n 3. As intensity of electric current increases, the magnetic lines of force become more clearer.\r\n" +
            "Inference: \r\n 1. If the current flows through the coil, ____ are produced at each point of the coil.\r\n 2. In above experiments, the intensity of magnetic field at any point by a current flowing through a coil, is dependent on the ____\r\n",

            //answers
            answer =
            "Observation:\r\n 1. magnetic fields \r\n 2. larger\r\n" +
            "Inference: \r\n 1. magnetic fields\r\n 2. umber of magnetic field lines passing through a unit area\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(MagQuestion);

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
public class MagDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<MagQuestion> questions;
    public string marks;
    public List<MagComment> comments;
}

[System.Serializable]
public class MagQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class MagComment
{
    public string name;
    public string comments;
}
