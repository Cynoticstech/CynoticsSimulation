using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Observe : MonoBehaviour
{
    public TMP_InputField[] answers;
    public SendApiExp sendApi;
    public TMP_Dropdown[] dropdown;

    public void ObsExpSend()
    {
        StartCoroutine(ObsAdd());
    }

    IEnumerator ObsAdd()
    {
        string apiUrl = "https://echo.backend.cynotics.in/api/experiments/";

        ObsDataSend data = new ObsDataSend
        {
            experimentName = "Oxidation and Addition Reaction",
            moduleName = "Oxidation of ethanol\r\n Addition reaction of fatty acids",
            user = Get_Student_Details.guid,
            questions = new List<ObsQuestion>()
        };

        ObsQuestion ONS = new ObsQuestion
        {
            //Questions
            question =
            "Observation 1:\r\n Sr. no, Experimental procedure, Observations\r\n" +
            "1, Reaction is endothermic or exothermic, ____\r\n" +
            "2, Initial temperature, ____\r\n" +
            "3, Final temperature, ____\r\n" +
            "4, Change in physical state, ____ to ____\r\n" +
            "Inference 1:\r\n 1. The reaction of water with lime is a ____ reaction.\r\n 2. Here lime and water react to form ____.\r\n" +

            "Observation 2:\r\n Sr. no, Experimental procedure, Observations\r\n" +
            "1, Original colour of the ferrous sulphate, ____\r\n" +
            "2, Colour of the gas evolved on heating, ____\r\n" +
            "3, Final colour of the substance in the test tube, ____\r\n" +
            "Inference 2:\r\n 1. On heating, the pale green coloured crystals of ferrous sulphate undergo decomposition. A mixture of ____ & ____ gases are formed.\r\n 2. A residue of ____ colour remains in the test tube.\r\n" +

            "Observation 3:\r\n Sr. no, Experimental procedure, Observations\r\n" +
            "1, Colour of the CuSO4 solution before the experiment, ____\r\n" +
            "2, Colour of the iron nail before the experiment, ____\r\n" +
            "3, Colour of the CuSO4 solution after the experiment, ____\r\n" +
            "4, Colour of the iron nail after the experiment, ____\r\n" +
            "Inference 3:\r\n 1. On immersing the grey coloured iron nails in blue coloured copper sulphate solution, they displace ____ from the copper sulphate solution and their colour becomes ____\r\n 2. This is a ____ reaction.\r\n" +

            "Observation 4:\r\n Sr. no, Experimental procedure, Observations\r\n" +
            "1, The colour and the nature of the sodium sulphate solution before the experiment., ____\r\n" +
            "2, The colour and the nature of the barium chloride solution before the experiment., ____\r\n" +
            "3, The colour and nature of the mixture resulting from the two solutions into each other., ____\r\n" +
            "Inference 4:\r\n 1.In this reaction, white coloured insoluble ____ is formed. As a result, a white coloured precipitate is formed in the beaker.",

            //answers
            answer =
            "Observation 1:\r\n" +
            "Exothermic\r\n 30\r\n 40\r\n Solid to Liquid\r\n" +
            "Inference 1:\r\n 1. combination \r\n 2. slaked lime\r\n" +
            "Observation 2:\r\n" +
            "Light Green\r\n Reddish Brown\r\n Reddish Brown\r\n" +
            "Inference 2:\r\n 1. sulphur dioxide & sulphur trioxide\r\n 2. reddish brown\r\n" +
            "Observation 3:\r\n" +
            "Blue\r\n Grey\r\n green\r\n Reddish Brown\r\n" +
            "Inference 3:\r\n 1. copper , reddish brown\r\n 2. single displacement/displacement\r\n" +
            "Observation 3:\r\n" +
            "colourless\r\n colourless\r\n white ppt\r\n" +
            "Inference 3:\r\n 1. precipitate/ppt\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(ONS);

        ONS.attemptedanswer.Add(dropdown[0].options[dropdown[0].value].text);
        ONS.attemptedanswer.Add(dropdown[1].options[dropdown[1].value].text);
        ONS.attemptedanswer.Add(dropdown[2].options[dropdown[2].value].text);
        ONS.attemptedanswer.Add(dropdown[3].options[dropdown[3].value].text + "to" + dropdown[4].options[dropdown[4].value].text);
        ONS.attemptedanswer.Add(answers[0].text);
        ONS.attemptedanswer.Add(answers[1].text);

        ONS.attemptedanswer.Add(dropdown[5].options[dropdown[5].value].text);
        ONS.attemptedanswer.Add(dropdown[6].options[dropdown[6].value].text);
        ONS.attemptedanswer.Add(dropdown[7].options[dropdown[7].value].text);
        ONS.attemptedanswer.Add(answers[2].text + "&" + answers[3]);
        ONS.attemptedanswer.Add(answers[4].text);

        ONS.attemptedanswer.Add(dropdown[8].options[dropdown[8].value].text);
        ONS.attemptedanswer.Add(dropdown[9].options[dropdown[9].value].text);
        ONS.attemptedanswer.Add(dropdown[10].options[dropdown[10].value].text);
        ONS.attemptedanswer.Add(dropdown[11].options[dropdown[11].value].text);
        ONS.attemptedanswer.Add(answers[5].text);
        ONS.attemptedanswer.Add(answers[6].text);
        ONS.attemptedanswer.Add(answers[7].text);

        ONS.attemptedanswer.Add(dropdown[9].options[dropdown[9].value].text);
        ONS.attemptedanswer.Add(dropdown[10].options[dropdown[10].value].text);
        ONS.attemptedanswer.Add(dropdown[11].options[dropdown[11].value].text);
        ONS.attemptedanswer.Add(answers[8].text);

        ObsComment comment = new ObsComment
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
public class ObsDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<ObsQuestion> questions;
    public string marks;
    public ObsComment comments;
}

[System.Serializable]
public class ObsQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class ObsComment
{
    public string name;
    public string comments;
}
