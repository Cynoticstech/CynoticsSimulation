using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IceManager : MonoBehaviour
{
    [SerializeField]
    float AnimationSpeed = 0.5f, meterScale = 4f,
        meterScaleTime = 3f, iceMeltTime = 1f, waterRiseTime = 5f, waterScale = 1.7f,
        logTime = 1f,maskTime=10f;
    [SerializeField] TMP_Text timeTxt, tempTxt;
    [SerializeField] float[] timeValues, tempValues;
    [SerializeField] Transform tempLine, waterLevel,maskObj;
    [SerializeField]
    float[] iceChangeTimes;
    [SerializeField] SpriteRenderer[] IceSprites;
    float animTimer = 0, logTimer;
    int curtSequence = 0, curtLogIndex = 0;
    [SerializeField] GameObject flameObj, SubmitButton;
    [SerializeField] GameObject AnswerContent, AnswerPrefab, AnswerHolder;
    public UnityEvent OnAnimComplete;
    public List<GameObject> ApiAnswerList;
    bool isAnimating = false, canAnimate = true,canLog = true;

    private void Update()
    {
        if (isAnimating)
        {
            animTimer += Time.deltaTime * AnimationSpeed;
            logTimer += Time.deltaTime;
            if (curtSequence < iceChangeTimes.Length)
            {
                if (animTimer > iceChangeTimes[curtSequence])
                {
                    curtSequence++;
                    IceSprites[curtSequence - 1].DOFade(0, iceMeltTime);
                    IceSprites[curtSequence - 1].transform.DOMoveY(IceSprites[curtSequence - 1].transform.position.y - 0.3f, iceMeltTime);
                    IceSprites[curtSequence - 1].transform.DOScaleY(0.7f, iceMeltTime);
                    IceSprites[curtSequence].DOFade(1, iceMeltTime);
                    if (curtSequence == iceChangeTimes.Length)
                    {
                        IceSprites[curtSequence].DOFade(0, iceMeltTime);
                    }
                }
            }
            if (curtLogIndex < tempValues.Length)
            {
                if (logTimer > logTime * curtLogIndex)
                {
                    canLog = true;
                    var _tim = timeValues[curtLogIndex];
                    var _tem = tempValues[curtLogIndex];
                    timeTxt.text = "Time: " + _tim + " min";
                    tempTxt.text = "Temprature: " + _tem + " <sup>o</sup>C";
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
    }

    public void LogValue()
    {
        if (!canLog||curtLogIndex>=timeValues.Length)
            return;
        var _tim = timeValues[curtLogIndex];
        var _tem = tempValues[curtLogIndex];
        DynamicDataHolder.Instance.LoggedTime.Add(_tim);
        DynamicDataHolder.Instance.LoggedTemp.Add(_tem);
        canLog = false;
    }

    public void BeginAnim()
    {
        if (!canAnimate||isAnimating)
            return;
        isAnimating = true;
        
        DynamicDataHolder.Instance.LoggedTemp = new List<float>();
        DynamicDataHolder.Instance.LoggedTime = new List<float>();
        tempLine.DOScaleX(meterScale, meterScaleTime);
        waterLevel.DOScaleX(waterScale, waterRiseTime);
        maskObj.transform.DOScaleX(0, maskTime);
        flameObj.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowAnswer()
    {
        ApiAnswerList.Clear();
        for (int i = 1; i < AnswerContent.transform.childCount - 2; i++)
        {
            /*if (i == AnswerContent.transform.childCount - 2 || i == 0)
            {
                continue;
            }*/
            Destroy(AnswerContent.transform.GetChild(i).gameObject);
        }
        var _temp = DynamicDataHolder.Instance;
        for (int i = 0; i < _temp.LoggedTemp.Count; i++)
        {
            var _obj = Instantiate(AnswerPrefab, AnswerContent.transform);
            _obj.transform.GetChild(0).GetComponent<TMP_Text>().text = _temp.LoggedTime[i].ToString();
            _obj.transform.GetChild(1).GetComponent<TMP_Text>().text = _temp.LoggedTemp[i].ToString();
            ApiAnswerList.Add(_obj);
        }
        //AnswerContent.transform.GetChild(2).SetSiblingIndex(AnswerContent.transform.childCount - 1);
        SubmitButton.transform.SetAsLastSibling();
        AnswerHolder.SetActive(true);
    }
}

