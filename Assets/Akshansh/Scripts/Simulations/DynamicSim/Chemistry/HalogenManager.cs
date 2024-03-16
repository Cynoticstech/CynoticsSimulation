using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class HalogenManager : MonoBehaviour
{
    [SerializeField] DragManager dropper;
    [System.Serializable]
    class BeakerHolder
    {
        public Transform BeakerTrans;
        public Collider2D BeakerCol;
        public bool IsSolutionHolder;
        [HideInInspector] public bool HasPickedSolution, HasFormedHalogen;
        public SpriteRenderer TargetSolution;
        public Color HalogenColor, WaterColor;
    }

    [SerializeField] BeakerHolder[] BeakersInScene;
    [SerializeField] Transform dropperLiquid, dropperDropPos;
    [SerializeField] GameObject halogenPref;
    [SerializeField]
    float dropperReturnTime = 2f, dropperPickSpeed = 1f, dropperDipSpeed = 1f,
        dropperFillTime = 1f, dropperFillScale = 0.5f, waterDropSpeed = 1f, halogenFormDelay = 0.5f,
        halogenColSpeed = 1f, waterChangeSpeed = 1f, beakerLiftTime = 1f, beakerRotAngle = 20f, beakerRotTime = 0.2f;
    [SerializeField] int beakerShakeItterations = 3;
    [SerializeField] Vector3 dropperPickOffset, dropperDipOffset, dropperTubeOffset, HalogenOffset, beakerLiftOffset;

    int curtBeakerRotIndex = 0;
    BeakerHolder solutionBeaker;
    Vector3 origDropperPos;
    Collider2D dropperCol;

    public UnityEvent OnExpComplete;
    public int completionIndex = 0;

    private void Start()
    {
        GetSolutionBeaker();
        dropperCol = dropper.GetComponent<Collider2D>();
        origDropperPos = dropper.transform.position;
        dropper.OnCorrectPlaced.AddListener(OnCorrectPlacement);
    }

    void GetSolutionBeaker()
    {
        foreach (var v in BeakersInScene)
        {
            if (v.IsSolutionHolder)
            {
                solutionBeaker = v;
                return;
            }
        }
    }

    int GetBeakerIndex(Collider2D _col)
    {
        int _index = 0;

        foreach (var v in BeakersInScene)
        {
            if (_col == v.BeakerCol)
            {
                return _index;
            }
            _index++;
        }
        return -1;
    }

    void OnCorrectPlacement()
    {
        int _beakerIndex = GetBeakerIndex(dropper.GetTargetCol());
        dropperCol.enabled = false;
        bool isSolutionBeaker = BeakersInScene[_beakerIndex] == solutionBeaker;
        if (isSolutionBeaker)
        {
            if (solutionBeaker.HasPickedSolution)//if already picked solution
            {
                ResetDropperPos();
                return;
            }
            else
            {
                dropperCol.enabled = false;
                solutionBeaker.HasPickedSolution = true;
                //animate dropper pickup
                PickLiquid();
            }
        }
        else
        {
            if (!solutionBeaker.HasPickedSolution)
            {
                ResetDropperPos();
                return;
            }
            else
            {
                var _tempBeaker = BeakersInScene[GetBeakerIndex(dropper.GetTargetCol())];
                if (_tempBeaker.HasFormedHalogen)
                {
                    ResetDropperPos();
                    return;
                }
                dropperCol.enabled = false;
                _tempBeaker.HasFormedHalogen = true;

                dropper.transform.DOMove(_tempBeaker.BeakerTrans.position + dropperTubeOffset,
                    dropperPickSpeed).onComplete += () =>
                    {
                        var _drop = Instantiate(halogenPref, dropperDropPos.position, Quaternion.identity);
                        _drop.transform.DOMove(_tempBeaker.BeakerTrans.position + HalogenOffset,
                            waterDropSpeed).onComplete += () =>
                            {
                                _drop.transform.parent = _tempBeaker.BeakerTrans;
                                //shake beaker
                                Vector3 _origPos = _tempBeaker.BeakerTrans.position;
                                _tempBeaker.BeakerTrans.DOMove(_origPos + beakerLiftOffset, beakerLiftTime).onComplete
                                += () =>
                                BeakerAnim(_tempBeaker, _drop, _origPos);
                            };

                        dropperLiquid.DOScaleY(0, dropperDipSpeed).onComplete += () => { ResetDropperPos();solutionBeaker.HasPickedSolution = false; };
                    };
            }
        }
    }

    private void BeakerAnim(BeakerHolder _tempBeaker, GameObject _drop, Vector3 _origPos)
    {
        _tempBeaker.BeakerTrans.DORotate(new Vector3(0, 0,-beakerRotAngle), beakerRotTime).SetEase(Ease.Linear).onComplete +=
        () =>
        {
            _tempBeaker.BeakerTrans.DORotate(new Vector3(0, 0,beakerRotAngle), beakerRotTime).SetEase(Ease.Linear).onComplete +=
            () =>
            {
                curtBeakerRotIndex++;
                if (curtBeakerRotIndex >= beakerShakeItterations)
                {
                    curtBeakerRotIndex = 0;
                    _tempBeaker.BeakerTrans.DORotate(Vector3.zero,beakerRotTime);
                    _tempBeaker.BeakerTrans.DOMove(_origPos, beakerLiftTime).onComplete
                    += () =>
                    {
                        _drop.GetComponent<SpriteRenderer>().DOColor(_tempBeaker.HalogenColor, halogenColSpeed).SetDelay(halogenFormDelay);//enable particles here
                        _tempBeaker.TargetSolution.DOColor(_tempBeaker.WaterColor, waterChangeSpeed).SetDelay(halogenFormDelay);
                        completionIndex++;
                        if (completionIndex > 2)
                        {
                            OnExpComplete?.Invoke();

                        }
                    };
                    return;
                }
                else
                {
                    BeakerAnim(_tempBeaker, _drop, _origPos);
                }
            };
        };

    }

    public void ResetDropperPos()
    {
        dropperCol.enabled = false;
        dropper.transform.DOMove(origDropperPos, dropperReturnTime).onComplete += ()
            =>
        {
            dropperCol.enabled = true;
        };
    }

    private void PickLiquid()
    {
        dropper.transform.DOMove(solutionBeaker.BeakerTrans.position + dropperPickOffset,
            dropperPickSpeed).onComplete += () =>
            {
                dropper.transform.DOMove(solutionBeaker.BeakerTrans.position + dropperDipOffset,
            dropperPickSpeed).onComplete += () =>
            {
                dropperLiquid.DOScaleY(dropperFillScale, dropperDipSpeed);
                dropper.transform.DOMove(solutionBeaker.BeakerTrans.position + dropperPickOffset,
            dropperPickSpeed).SetDelay(dropperFillTime).onComplete += () =>
            {
                ResetDropperPos();
            };
            };
            };
    }
}
