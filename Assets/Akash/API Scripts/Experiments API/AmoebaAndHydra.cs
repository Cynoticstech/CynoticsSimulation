using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AmoebaAndHydra : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    public SendApiExp sendApi;

    public void AmoebaHydraExpSend()
    {
        StartCoroutine(AmoebaHydra());
    }

    IEnumerator AmoebaHydra()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";

        AmoebaDataSend data = new AmoebaDataSend
        {
            experimentName = "Amoeba and Hydara",
            moduleName = "Amoeba and Hydara",
            user = Get_Student_Details.guid,
            questions = new List<AmoebaQuestion>()
        };

        AmoebaQuestion amoebaAndHydra = new AmoebaQuestion
        {
            //Questions
            question = 
            "Binary fission of amoeba:\r\n Stage 1\r\n Stage 2\r\n Stage 3\r\n Stage 4\r\n Stage 5\r\n" +
            "Observation:\r\n A. Binary fission in Amoeba:\r\n 1. At the beginning, the ____ cell of Amoeba  gets elongated.\r\n 2. Nucleus gets an ____ shaped.\r\n 3. A notch is developed at the site of division.\r\n 4. Two small ____ cells of Amoeba are formed." +
            "Hydra:\r\n Stage 1\r\n Stage 2\r\n Stage 3\r\n Stage 4\r\n Stage 5\r\n Stage 6\r\n Stage 7\r\n Stage 8\r\n Stage 9\r\n Stage 10\r\n Stage 11\r\n Stage 12\r\n" +
            "Observation:\r\n B. Budding in hydra \r\n 1. A small outgrowth is seen on the body of Hydra.\r\n 2. It becomes ____ due to enough growth.\r\n 3. This outgrowth detaches in the form of ____ having ____ and tentacles.\r\n" +
            "Inference:\r\n 1. Amoeba and Hydra reproduce by ____ method.\r\n",

            //answers
            answer =
            "Binary fission of amoeba:\r\nMother Amoeba\r\nPseudopodia are pulled in\r\nNucleus divides\nCytoplasm divides\r\nDaughter Amoeba\r\n" +
            "Observation:\r\n A. Binary fission in Amoeba: \r\n 1. Parent\r\n 2.Oval\r\n 3.Daughter" +
            "Hydra:\r\n Bud\r\n Mouth\r\n Hypostome\r\n Tentacle\r\n Nematocyst\r\n Testis\r\n Gastrovascular cavity\r\n Ovary\r\n Epidermis\r\n Gastrodermis\r\n Mesogleva\r\n Basal\r\n disc\r\n" +
            "Observation:\r\n B. Budding in Hydra: \r\n 1. multicellular \r\n 2. Bud \r\n 3. Mouth \r\n" +
            "Inference: \r\n 1. Asexual\r\n",
            
            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(amoebaAndHydra);
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
public class AmoebaDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<AmoebaQuestion> questions;
    public string marks;
    public List<AmoebaComment> comments;
}

[System.Serializable]
public class AmoebaQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class AmoebaComment
{
    public string name;
    public string comments;
}