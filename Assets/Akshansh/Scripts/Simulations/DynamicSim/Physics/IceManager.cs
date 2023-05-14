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
        logTime = 1f;
    [SerializeField] TMP_Text timeTxt, tempTxt;
    [SerializeField]
    float[] timeValues, tempValues;
    [SerializeField] Transform tempLine, waterLevel;
    [SerializeField]
    float[] iceChangeTimes;
    [SerializeField] SpriteRenderer[] IceSprites;
    float animTimer = 0, logTimer;
    int curtSequence = 0, curtLogIndex = 0;
    [SerializeField] GameObject flameObj;
    [SerializeField] GameObject AnswerContetent, IcePrefab;
    public UnityEvent OnAnimComplete;

    bool isAnimating = false, canAnimate = true;

    private void Update()
    {
        if (isAnimating)
        {
            animTimer += Time.deltaTime * AnimationSpeed;
            logTimer += Time.deltaTime;
            if (animTimer > iceChangeTimes[curtSequence] && curtSequence < iceChangeTimes.Length - 1)
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
            if (curtLogIndex < tempValues.Length)
            {
                if (logTimer > logTime * curtLogIndex)
                {
                    var _tim = timeValues[curtLogIndex];
                    var _tem = tempValues[curtLogIndex];
                    timeTxt.text = "Time: " + _tim;
                    tempTxt.text = "Temprature: " + _tem;
                    PhysicsData.Instance.LoggedTime.Add(_tim);
                    PhysicsData.Instance.LoggedTemp.Add(_tem);
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
    public void BeginAnim()
    {
        if (!canAnimate)
            return;
        isAnimating = true;
        PhysicsData.Instance.LoggedTemp = new List<float>();
        PhysicsData.Instance.LoggedTime = new List<float>();
        tempLine.DOScaleX(meterScale, meterScaleTime);
        waterLevel.DOScaleX(waterScale, waterRiseTime);
        flameObj.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

