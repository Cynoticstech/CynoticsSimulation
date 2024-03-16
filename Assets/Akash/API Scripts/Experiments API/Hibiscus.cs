using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Hibiscus : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    public SendApiExp sendApi;
    public void HibiscusExpSend()
    {
        StartCoroutine(Hibiscusc());
    }

    IEnumerator Hibiscusc()
    {
        string apiUrl = "https://echo.backend.cynotics.in/api/experiments/";

        HibiscusDataSend data = new HibiscusDataSend
        {
            experimentName = "Hibiscus",
            moduleName = "Hibiscus",
            user = Get_Student_Details.guid,
            questions = new List<HibiscusQuestion>()
        };

        HibiscusQuestion hibisQuestions = new HibiscusQuestion
        {
            //Questions
            question = 
            "Parts of flower: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n Part 8\r\n Part 9\r\n Part 10\r\n Part 11\r\n Part 12\r\n" +
            "Observation Table: \r\n " +
            "Whorl, Number of components, Structure, Function \r\n" +
            "Calyx, Five, Green sepals, ____\r\n" +
            "Corolla, ____, ____, ____\r\n" +
            "Androecium, Innumerable stamens, Anthers, To produce pollen grains\r\n" +
            "Gynoecium, ____, ____, ____\r\n" +
            "Inference:\r\n 1. ____ from flower is male whorl, while ____ is female whorl.\r\n Male gamete from pollen grain and egg cell from ovule, these two haploid cells unite to form a diploid zygote. Thus the process of seed and fruit formation begins.\r\n Flower is the reproductive organ of plants.\r\n",
            
            //answers
            answer =
            "Parts of flower: \r\n Filament\r\n Stamen\r\n Anther\r\n Petal\r\n Stigma\r\n Style\r\n Pistil\r\n Ovary\r\n Ovule\r\n Peduncle\r\n Receptacle\r\n Petal\r\n" +
            "Observation Table: \r\n" +
            "Whorl, Number of components, Structure, Function \r\n" +
            "Calyx, Five, Green sepals, It houses the young bud\r\n" +
            "Corolla, Five, red in colour, To attract the insects\r\n" +
            "Androecium, Innumerable stamens, Anthers, To produce pollen grains\r\n" +
            "Gynoecium, Five carpels, multicarpellary and syncarpous, receives the pollen from the stamen and ovules contain the egg cell for fertilisation\r\n" +
            "Inference:\r\n 1. Androecium  \r\n 2.Gynoecium \r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(hibisQuestions);
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
        HibiscusComment comment = new HibiscusComment
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
public class HibiscusDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<HibiscusQuestion> questions;
    public string marks;
    public HibiscusComment comments;
}

[System.Serializable]
public class HibiscusQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class HibiscusComment
{
    public string name;
    public string comments;
}
