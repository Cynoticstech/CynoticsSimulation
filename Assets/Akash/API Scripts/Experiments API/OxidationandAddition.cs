using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OxidationandAddition : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;
    public TMP_Dropdown[] dropdown;
    public void OxiAddExpSend()
    {
        StartCoroutine(OxiAdd());
    }

    IEnumerator OxiAdd()
    {
        string apiUrl = "https://echo.backend.cynotics.in/api/experiments/";

        OxiAddDataSend data = new OxiAddDataSend
        {
            experimentName = "Oxidation and Addition Reaction",
            moduleName = "Oxidation of ethanol\r\n Addition reaction of fatty acids",
            user = Get_Student_Details.guid,
            questions = new List<OxiAddQuestion>()
        };

        OxiAddQuestion OxiAddQuestion = new OxiAddQuestion
        {
            //Questions
            question =
            "Oxidation of ethanol\r\n Observation:\r\n A. Potassium permanganate oxidises ethanol to ____. As potassium permanganate is consumed in this reaction, its ____ colour vanishes\r\n" +
            "Addition reaction of fatty acids \r\n Observation Table: \r\n Oil sample, Colour change observe in solution\r\n" +
            "Groundnut oil, ____\r\n" +
            "Safflower oil, ____\r\n" +
            "Sunflower  oil, ____\r\n" +
            "Vanaspati ghee, ____\r\n" +
            "Inference:\r\n Iodine is consumed due to its addition to fatty acid. Therefore the coloured solution becomes colourless. But when the same procedure is followed for vanaspati ghee, a similar colour change is not observed. As vanaspati ghee is saturated hydrocarbon, the ____ reaction does not take place.\r\n",

            //answers
            answer =
            "Oxidation of ethanol\r\n Observation:\r\n 1. ethanoic acid\r\n 2. pink " +
            "Addition reaction of fatty acids \r\n Observation Table: \r\n Oil sample, Colour change observe in solution\r\n" +
            "Groundnut oil, tincture iodine colour vanishes\r\n" +
            "Safflower oil, tincture iodine colour vanishes\r\n" +
            "Sunflower  oil, tincture iodine colour vanishes\r\n" +
            "Vanaspati ghee,  tincture iodine colour does not vanishes\r\n" +
            "Inference:\r\n 1. addition \r\n",
            
            //attempted answers
            attemptedanswer = new List<string>()
        };
        
        data.questions.Add(OxiAddQuestion);
        OxiAddQuestion.attemptedanswer.Add(dropdown[0].options[dropdown[0].value].text);
        OxiAddQuestion.attemptedanswer.Add(dropdown[1].options[dropdown[1].value].text);
        OxiAddQuestion.attemptedanswer.Add(dropdown[2].options[dropdown[2].value].text);
        OxiAddQuestion.attemptedanswer.Add(dropdown[3].options[dropdown[3].value].text);

        foreach (TMP_InputField answerField in answers)
        {
            data.questions[0].attemptedanswer.Add(answerField.text);
        }
        int emptyCount = answers.Length - answers.Length;
        for (int i = 0; i < emptyCount; i++)
        {
            data.questions[0].attemptedanswer.Add("");
        }
        OxiAddComment comment = new OxiAddComment
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
public class OxiAddDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<OxiAddQuestion> questions;
    public string marks;
    public OxiAddComment comments;
}

[System.Serializable]
public class OxiAddQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class OxiAddComment
{
    public string name;
    public string comments;
}
