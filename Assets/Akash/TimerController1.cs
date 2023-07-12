using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics.Contracts;

public class TimerController1 : MonoBehaviour
{
    public float gameTimeScale = 60f;
    [SerializeField] TMP_Text t1Txt, t2Txt;
    [SerializeField] float tempChangeTime = 1f;
    [SerializeField] float[] t1Value, t2Value;
    bool updatingTemp;

    int tempindex;
    private float timer = 0f; 
    public TMP_Text timerText;
    //public static bool startTimer = false;

    private void Start()
    {
        //timerText = GetComponent<TMP_Text>();
        UpdateTimerText();
        t1Txt.text = "Temprature T1: " + t1Value[tempindex] + " <sup>o</sup>C";
        t2Txt.text = "Temprature T2: " + t2Value[tempindex] + " <sup>o</sup>C";
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
        if(!updatingTemp)
        {
            StartCoroutine(UpdateTemp());
            var _data = DynamicDataHolder.Instance;
            _data.meltT1.Clear();//new log if new exp started
            _data.meltT2.Clear();
        }
    }
    IEnumerator UpdateTemp()
    {
        updatingTemp = true;
        if(tempindex<t1Value.Length-1)
        {
            yield return new WaitForSeconds(5);
            tempindex++;
            t1Txt.text = "Temprature T1: " + t1Value[tempindex] + " <sup>o</sup>C";
            t2Txt.text = "Temprature T2: " + t2Value[tempindex] + " <sup>o</sup>C";
            StartCoroutine(UpdateTemp());
        }
    }

    public void LogValue()
    {
        var _data = DynamicDataHolder.Instance;
        foreach (var v in _data.meltT1)
        {
            if (v == t1Value[tempindex])
                return;
        }

        _data.meltT1.Add(t1Value[tempindex]);
        _data.meltT2.Add(t2Value[tempindex]);
    }
}
