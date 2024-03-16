using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WaterBoilMang : MonoBehaviour
{
    [SerializeField]
    float CooldownDelay = 5f, meterScale = 4f,
        meterScaleTime = 3f, iceMeltTime = 1f, waterRiseTime = 5f, waterScale = 1.7f,
        logTime = 1f, MeterUpScale =1f,graphTime =12.5f;
    [SerializeField] TMP_Text timeTxt, tempTxt;
    [SerializeField]
    float[] timeValues, tempValues;
    [SerializeField] Transform tempLine, waterLevel,graphMask;
    [SerializeField]
    float animTimer = 0, logTimer;
    int curtLogIndex = 0;
    [SerializeField] GameObject flameObj, SubmitButton;
    [SerializeField] GameObject AnswerContent, AnswerPrefab, AnswerHolder;
    public UnityEvent OnAnimComplete;

    public List<GameObject> ApiAnswers;
    bool isAnimating = false, canAnimate = true, canLog, swapped;

    private void Update()
    {
        if (isAnimating)
        {
            if (animTimer > CooldownDelay)
            {
                if (!swapped)
                {
                    tempLine.DOScaleX(meterScale, meterScaleTime);
                    graphMask.DOScaleX(0,graphTime);
                    flameObj.SetActive(false);
                    swapped = true;
                }
                logTimer += Time.deltaTime;
                if (curtLogIndex < tempValues.Length)
                {
                    if (logTimer > logTime * curtLogIndex)
                    {
                        var _tim = timeValues[curtLogIndex];
                        var _tem = tempValues[curtLogIndex];
                        timeTxt.text = "Time: " + _tim +"min";
                        tempTxt.text = "Temprature: " + _tem +"<sup>0</sup>";
                        DynamicDataHolder.Instance.LoggedTime.Add(_tim);
                        DynamicDataHolder.Instance.LoggedTemp.Add(_tem);
                        curtLogIndex++;
                    }
                }
                else
                {
                    OnAnimComplete?.Invoke();
                    isAnimating = false;
                    canAnimate = false;
                }
            }
            else
            {
                animTimer += Time.deltaTime;
            }
        }
    }
    public void BeginAnim()
    {
        if (!canAnimate)
            return;
        isAnimating = true;
        DynamicDataHolder.Instance.LoggedTemp = new List<float>();
        DynamicDataHolder.Instance.LoggedTime = new List<float>();
        tempLine.DOScaleX(MeterUpScale, CooldownDelay);
        waterLevel.DOScaleX(waterScale, waterRiseTime);
        flameObj.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowAnswer()
    {
        ApiAnswers.Clear();

        for (int i = 1; i < AnswerContent.transform.childCount - 3; i++)
        {
            Destroy(AnswerContent.transform.GetChild(i).gameObject);
        }
        var _temp = DynamicDataHolder.Instance;
        for (int i = 0; i < _temp.LoggedTemp.Count; i++)
        {
            var _obj = Instantiate(AnswerPrefab, AnswerContent.transform);
            _obj.transform.GetChild(0).GetComponent<TMP_Text>().text = _temp.LoggedTime[i].ToString();
            _obj.transform.GetChild(1).GetComponent<TMP_Text>().text = _temp.LoggedTemp[i].ToString();

            ApiAnswers.Add(_obj);
        }
        AnswerContent.transform.GetChild(2).SetSiblingIndex(AnswerContent.transform.childCount - 1);
        SubmitButton.transform.SetAsLastSibling();
        AnswerHolder.SetActive(true);
    }
    public void LogValue()
    {
        if (!canLog || curtLogIndex >= timeValues.Length)
            return;
        var _tim = timeValues[curtLogIndex];
        var _tem = tempValues[curtLogIndex];
        DynamicDataHolder.Instance.LoggedTime.Add(_tim);
        DynamicDataHolder.Instance.LoggedTemp.Add(_tem);
        canLog = false;
    }
}

