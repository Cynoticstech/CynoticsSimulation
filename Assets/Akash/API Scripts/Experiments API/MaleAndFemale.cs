using Simulations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MaleAndFemale : MonoBehaviour
{
    public TMP_InputField[] answers;
    [SerializeField] SimulationSetupManager simulationSetupManager;
    public SendApiExp sendApi;

    public void MFExpSend()
    {
        StartCoroutine(MF());
    }

    IEnumerator MF()
    {
        string apiUrl = "https://echo.backend.cynotics.in/api/experiments/";

        MFDataSend data = new MFDataSend
        {
            experimentName = "Male and Female",
            moduleName = "Male and Female",
            user = Get_Student_Details.guid,
            questions = new List<MFQuestion>()
        };

        MFQuestion mfQuestions = new MFQuestion
        {
            //Questions
            question =
            "Male organs: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n Part 8\r\n" +
            "Observation Table: \r\n Sr., Name of organ in Male Reproductive System, Functions\r\n" +
            "1, Testes, ____\r\n" +
            "2, Vas deferens, ____\r\n" +
            "3, Seminal Vesicles, ____\r\n" +
            "4, Prostate gland, ____\r\n" +
            "5, Urethra, ____\r\n" +
            "6, Penis, ____\r\n" +
            "Female  organs: \r\n Part 1\r\n Part 2\r\n Part 3\r\n Part 4\r\n Part 5\r\n Part 6\r\n Part 7\r\n Part 8\r\n"+
            "Observation Table: \r\n Sr., Name of organ in Female Reproductive System, Functions\r\n" +
            "1, Ovaries, ____\r\n" +
            "2, Fallopian tubes, ____\r\n" +
            "3, Uterus, ____\r\n" +
            "4, Cervix, ____\r\n" +
            "5, Vagina, ____\r\n" +
            "6, Clitoris, ____\r\n" +
            "7, Labia, ____\r\n" +
            "Inference: \r\n 1. Male reproductive system produces haploid gametes called ____.\r\n 2. ____ reproductive system produces haploid(n) gametes called ____.\r\n 3. ____ And ____ cell unite to form a diploid (2n) zygote and afterwards it develops into human female embryo.\r\n",

            //answers
            answer =
            "Male organs: \r\n Vas deferens\r\n prostate\r\n Seminal Vesicle\r\n Bladder\r\n Urethra\r\n Penis\r\n Epidydimis\r\nTesticle\r\n"+
            "Observation Table: \r\n Sr., Name of organ in Male Reproductive System, Functions\r\n" +
            "1, Testes, Stores and transports sperm from the testes to the vas deferens\r\n" +
            "2, Vas deferens, Transports sperm from the epididymis to the urethra\r\n" +
            "3, Seminal Vesicles, Secrete fluid that nourishes and protects sperm.\r\n" +
            "4, Prostate gland, Produces fluid that helps to nourish and protect sperm.\r\n" +
            "5, Urethra, Carries urine and semen out of the body.\r\n" +
            "6, Penis, Delivers semen to the female reproductive system during sexual intercourse.\r\n"+
            "Female  organs: \r\n Endometrium \r\n Ovary \r\n Fallopian tubes\r\n Uterus\r\n Fimbriae\r\n Ovary\r\n Cervix\r\n Vagina\r\n" +
            "Observation Table: \r\n Sr., Name of organ in Female Reproductive System, Functions\r\n" +
            "1, Ovaries, Produce eggs and hormones (estrogen and progesterone).\r\n" +
            "2, Fallopian tubes, Transport eggs from the ovaries to the uterus and serve as the site of fertilization.\r\n" +
            "3, Uterus, Nurtures and houses the developing fetus during pregnancy\r\n" +
            "4, Cervix, Connects the uterus to the vagina and serves as a barrier to the outside world during pregnancy.\r\n" +
            "5, Vagina, Serves as the birth canal and the site of sexual intercourse.\r\n" +
            "6, Clitoris, A highly sensitive organ that plays a role in sexual arousal.\r\n" +
            "7, Labia, Outer and inner folds of skin that protect the vaginal opening.\r\n" +
            "Inference: \r\n 1. sperms \r\n 2. Female  \r\n 3. oocyte \r\n 4. Sperm  \r\n 5. Egg\r\n",

            //attempted answers
            attemptedanswer = new List<string>()
        };
        data.questions.Add(mfQuestions);
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
        MFComment comment = new MFComment
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
public class MFDataSend
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<MFQuestion> questions;
    public string marks;
    public MFComment comments;
}

[System.Serializable]
public class MFQuestion
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class MFComment
{
    public string name;
    public string comments;
}