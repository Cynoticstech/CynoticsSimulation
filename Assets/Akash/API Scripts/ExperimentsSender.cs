using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ExperimentSender : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SendExperimentsData());
    }

    IEnumerator SendExperimentsData()
    {
        string apiUrl = "https://echo-admin-backend.vercel.app/api/experiments/";
        List<ExperimentsData> experimentsData = new List<ExperimentsData>();

        ExperimentsData data1 = new ExperimentsData
        {
            ExperimentName = "Experiment 1",
            ModuleName = "Module A",
            User = "John Doe",
            Questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Question 1",
                    Answer = "Answer 1",
                    AttemptedAnswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2"
                    }
                },
                new Question
                {
                    QuestionText = "Question 2",
                    Answer = "Answer 2",
                    AttemptedAnswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2",
                        "Attempt 3"
                    }
                }
            },
            Marks = "90",
            Comments = new List<Comment>
            {
                new Comment
                {
                    Name = "John",
                    CommentText = "Comment 1"
                }
            }
        };
        experimentsData.Add(data1);

        ExperimentsData data2 = new ExperimentsData
        {
            ExperimentName = "Experiment 2",
            ModuleName = "Module B",
            User = "Jane Smith",
            Questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Question 3",
                    Answer = "Answer 3",
                    AttemptedAnswer = new List<string>
                    {
                        "Attempt 1"
                    }
                },
                new Question
                {
                    QuestionText = "Question 4",
                    Answer = "Answer 4",
                    AttemptedAnswer = new List<string>
                    {
                        "Attempt 1",
                        "Attempt 2",
                        "Attempt 3",
                        "Attempt 4"
                    }
                }
            },
            Marks = "80",
            Comments = new List<Comment>
            {
                new Comment
                {
                    Name = "Jane",
                    CommentText = "Comment 2"
                },
                new Comment
                {
                    Name = "John",
                    CommentText = "Comment 3"
                }
            }
        };
        experimentsData.Add(data2);

        string jsonData = JsonUtility.ToJson(experimentsData.ToArray());
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
    public string ExperimentName;
    public string ModuleName;
    public string User;
    public List<Question> Questions;
    public string Marks;
    public List<Comment> Comments;
}

[System.Serializable]
public class Question
{
    public string QuestionText;
    public string Answer;
    public List<string> AttemptedAnswer;
}

[System.Serializable]
public class Comment
{
    public string Name;
    public string CommentText;
}
