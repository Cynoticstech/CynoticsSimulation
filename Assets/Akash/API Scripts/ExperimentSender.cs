using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ExperimentSender : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(SendExperimentsData());
    }

    public void expSend()
    {
        StartCoroutine(SendExperimentsData());
    }

    IEnumerator SendExperimentsData()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";
        List<ExperimentsData> experimentsData = new List<ExperimentsData>();
        //ExperimentsData expDatum = new ExperimentsData();


        ExperimentsData data1 = new ExperimentsData
        {
            experimentName = "Experiment 1",
            moduleName = "Module A",
            user = Get_Student_Details.guid,
            questions = new List<Question>
            {
                new Question
                {
                    question = "Question 1",
                    answer = "Answer 1",
                    attemptedanswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2"
                    }
                },
                new Question
                {
                    question = "Question 2",
                    answer = "Answer 2",
                    attemptedanswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2",
                        "Attempt 3"
                    }
                }
            }
        };
        experimentsData.Add(data1);

        ExperimentsData data2 = new ExperimentsData
        {
            experimentName = "Experiment 2",
            moduleName = "Module B",
            user = Get_Student_Details.guid,
            questions = new List<Question>
            {
                new Question
                {
                    question = "Question 3",
                    answer = "Answer 3",
                    attemptedanswer = new List<string>
                    {
                        "Attempt 1"
                    }
                },
                new Question
                {
                    question = "Question 4",
                    answer = "Answer 4",
                    attemptedanswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2",
                        "Attempt 3",
                        "Attempt 4"
                    }
                }
            },
            
        };
        experimentsData.Add(data2);

        string jsonData = JsonUtility.ToJson(experimentsData.ToArray());
        //string jsonData = JsonUtility.ToJson(experimentsData);
        //string jsonData = JsonUtility.ToJson(data1);
        /*byte[] rawBody = Encoding.UTF8.GetBytes(jsonData);
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();*/
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonData);
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
public class ExperimentsData
{
    public string experimentName;
    public string moduleName;
    public string user;
    public List<Question> questions;
    public string marks;
    public List<Comment> comments;
}

[System.Serializable]
public class Question
{
    public string question;
    public string answer;
    public List<string> attemptedanswer;
}

[System.Serializable]
public class Comment
{
    public string name;
    public string comments;
}
