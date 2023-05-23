using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ReactivityManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown[] ObservationInputs;
    [SerializeField] GameObject ObservationObj;
    [SerializeField] TMP_Dropdown MetalType;
    [SerializeField] DragManager dropper;
    [SerializeField] Vector3 saltDropperOffset, saltOffset;
    [SerializeField]
    float dropperMoveTime = 1f, dropperOrgPosTime = 3f, saltFallTime = 2f,
        saltColorChangeDelay = 1f, saltColorChangeSpeed = 2f, waterColorChangeDelay = 1f,
        waterColorChangeSpeed = 2f, ClockRotateTime = 2f;
    [SerializeField] Transform saltDropPos, saltPref;
    [SerializeField] Transform ClockHand;
    [SerializeField] GameObject ClockObject;
    [SerializeField] int maxClockItterations = 2;
    [System.Serializable]
    class SaltBeakerHolder
    {
        [SerializeField] TMP_Text InterfaceTxt;
        [SerializeField] string[] InterFaceTexts;
        public GameObject BeakerInterFace;
        public Collider2D BeakerCol;
        public Transform TargetTrans;
        public Color SaltColor, WaterCol, NormalSalt, NormalWater;
        public SpriteRenderer WaterRend;
        public int NonReactiveSaltIndex;
        [HideInInspector] public bool HaveMetal = false;
        public void UpdateInterface(int _index)
        {
            InterfaceTxt.text = InterFaceTexts[_index];
        }
    }
    [SerializeField]
    SaltBeakerHolder[] BeakersInScene;

    Collider2D dropperCol;
    int curtBeakerIndex, curtClockItteration = 0;
    Vector3 origDropperPos;

    public UnityEvent<int> OnAnimationCompleted;


    private void Start()
    {
        UpdateInterfaces();
        MetalType.onValueChanged.AddListener((_) => UpdateInterfaces());
        OnAnimationCompleted.AddListener((_index) => { BeakersInScene[_index].BeakerInterFace.SetActive(true); });
        dropper.OnCorrectPlaced.AddListener(CorrectPlacement);
        dropperCol = dropper.GetComponent<Collider2D>();
        origDropperPos = dropper.transform.position;

        foreach (var v in ObservationInputs)
        {
            v.onValueChanged.AddListener((val) => { AssignObservationListners(val, v); });
        }
    }
    public void ShowObservation()
    {
        foreach (var v in DynamicDataHolder.Instance.ReactivityData)
        {
            for (int i = 0; i < ObservationInputs.Length; i++)
            {
                if (i == v.DropDownIndex)
                {
                    //contains it in list
                    ObservationInputs[i].value = v.AnswerIndex;
                }
            }
        }

        ObservationObj.SetActive(true);
    }
    private void AssignObservationListners(int _val, TMP_Dropdown _drop)
    {
        var _dropDownIndex = 0;
        for (int i = 0; i < ObservationInputs.Length; i++)
        {
            if (_drop != ObservationInputs[i])
                continue;
            _dropDownIndex = i;
            break;
        }
        for (int i = 0; i < ObservationInputs.Length; i++)
        {
            for (int j = 0; j < DynamicDataHolder.Instance.ReactivityData.Count; j++)
            {
                if (DynamicDataHolder.Instance.ReactivityData[j].DropDownIndex == _dropDownIndex)
                {
                    print("Found Data");
                    DynamicDataHolder.Instance.ReactivityData[j].AnswerIndex = _val;
                    return;
                }
            }
        }
    DynamicDataHolder.Instance.ReactivityData.Add(
    new DynamicDataHolder.ReactivityDataHolder
    {
        AnswerIndex = _val,
        DropDownIndex = _dropDownIndex,
    });

    }

    private void UpdateInterfaces()
    {
        foreach (var v in BeakersInScene)
        {
            v.UpdateInterface(MetalType.value);
        }
    }

    int GetBeakerIndex(Collider2D col)
    {
        for (int i = 0; i < BeakersInScene.Length; i++)
        {
            if (col == BeakersInScene[i].BeakerCol)
            {
                return i;
            }
        }
        return -1;
    }
    void CorrectPlacement()
    {
        curtBeakerIndex = GetBeakerIndex(dropper.GetTargetCol());
        if (curtBeakerIndex == -1)
            return;
        var _tempBeaker = BeakersInScene[curtBeakerIndex];

        if (_tempBeaker.HaveMetal || ClockObject.activeInHierarchy)
        {
            dropperCol.transform.DOMove(origDropperPos, dropperOrgPosTime).onComplete +=
            () =>
            {
                dropperCol.enabled = true;
            };
            return;
        }
        dropperCol.enabled = false;
        dropperCol.transform.DOMove(_tempBeaker.TargetTrans.position + saltDropperOffset, dropperMoveTime).onComplete += () =>
        {
            dropper.transform.GetChild(0).gameObject.SetActive(false);
            DropSalt(); dropperCol.transform.DOMove(origDropperPos, dropperOrgPosTime).onComplete +=
            () =>
            {
                dropperCol.enabled = true;
                dropper.transform.GetChild(0).gameObject.SetActive(true);
            };
        };
    }

    void DropSalt()
    {
        var _tempBeaker = BeakersInScene[curtBeakerIndex];
        if (_tempBeaker.HaveMetal)
            return;
        var _temp = Instantiate(saltPref, saltDropPos.transform.position, Quaternion.identity);
        var _saltCol = _tempBeaker.NonReactiveSaltIndex != MetalType.value ? _tempBeaker.SaltColor : _tempBeaker.NormalSalt;
        var _waterCol = _tempBeaker.NonReactiveSaltIndex != MetalType.value ? _tempBeaker.WaterCol : _tempBeaker.NormalWater;
        _temp.transform.DOMove(_tempBeaker.TargetTrans.position + saltOffset, saltFallTime);
        _temp.GetComponent<SpriteRenderer>().DOColor(_saltCol, saltColorChangeSpeed).SetDelay(saltColorChangeDelay);
        _tempBeaker.WaterRend.DOColor(_waterCol, waterColorChangeSpeed).SetDelay(waterColorChangeDelay);
        _tempBeaker.HaveMetal = true;
        RotateClock(curtBeakerIndex);
    }

    void RotateClock(int _index)
    {
        ClockObject.SetActive(true);
        ClockHand.DORotate(new Vector3(0, 0, -180), ClockRotateTime, RotateMode.Fast).SetEase(Ease.Linear).onComplete +=
            () =>
            {
                ClockHand.DORotate(new Vector3(0, 0, 0), ClockRotateTime).SetEase(Ease.Linear).onComplete +=
                () =>
                {
                    curtClockItteration++;
                    if (curtClockItteration >= maxClockItterations)
                    {
                        curtClockItteration = 0;
                        ClockObject.SetActive(false);
                        OnAnimationCompleted?.Invoke(_index);
                        return;
                    }
                    else
                    {
                        RotateClock(_index);
                    }
                };
            };
    }

    public void LoadScene(string _name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_name);
    }
}
