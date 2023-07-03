using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;

public class TimerController1 : MonoBehaviour
{
    public float gameTimeScale = 60f; 
    private float timer = 0f; 
    public TMP_Text timerText;
    //public static bool startTimer = false;

    private void Start()
    {
        //timerText = GetComponent<TMP_Text>();
        UpdateTimerText();
    }

    private void Update()
    {
        /*if(!startTimer)
        {
            return;
        }*/

        timer += Time.deltaTime * gameTimeScale;

        if (timer >= 600f)
        {
            timer = 600f;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("Time: {0:00}min:{1:00}sec", minutes, seconds);
    }
}
