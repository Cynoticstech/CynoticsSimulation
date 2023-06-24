using AkshanshKanojia.Inputs.Button;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OxidationManager : MonoBehaviour
{
    [SerializeField] DragManager tube, beaker1, beaker2, kmno4;
    [SerializeField]
    float beakerDropSpeed = 1f, beakerRotSpeed = 2f, beakerResetTime = 4f, beakerDropDuration = 4f,
        tubePosTime = 1f, chemDropLiftTime = 1f, chemDropIdleTime = 2f, chemRotTime = 2f, tubeLiftTime = 1f, tubeShakeTime = 0.3f,
        tubeShakeIdle = 2f;
    [SerializeField] int curtLiquidIndex = 0, tubeShakeItteration = 3,experimentEndIndex =5;
    [SerializeField] Transform liquidMasks;
    [SerializeField] GameObject flameObj;
    [SerializeField] ButtonInputManager burnerButt;
    [SerializeField] SpriteRenderer tubeLiquid;
    [SerializeField] Color chemPink, tubeNormal;

    [SerializeField]
    float[] liquidLevels;
    [SerializeField] Vector3 dropPos, dropRot, tubePos, chemDropPos, chemRotAngle, tubeShakePos, tubeShakeRot;

    public UnityEvent OnSimulationFinished;

    int curtShakeItteration;
    private void Start()
    {
        AssigneEvents();
    }

    private void AssigneEvents()
    {
        beaker1.OnCorrectPlaced.AddListener(() => OnCorrectBeaker(beaker1));
        beaker2.OnCorrectPlaced.AddListener(() => OnCorrectBeaker(beaker2));
        tube.OnCorrectPlaced.AddListener(TubePlaced);
        burnerButt.OnTap += (obj) =>
        {
            BurnerTapped();
        };
        kmno4.OnCorrectPlaced.AddListener(ChemicalDrop);
    }
    void ChemicalDrop()
    {
        kmno4.DisableInteract();
        CheckLiquidIndex();//don't change order
        DropChem();
    }

    private void DropChem()
    {
        var _tempTrans = kmno4.transform;
        _tempTrans.DOMove(chemDropPos, chemDropLiftTime).onComplete += () =>
        {
            _tempTrans.DORotate(chemRotAngle, chemRotTime).SetEase(Ease.Linear).onComplete += () =>
            {
                _tempTrans.DORotate(Vector3.zero, chemRotTime).SetEase(Ease.Linear).SetDelay(chemDropIdleTime).onComplete += () =>
                {
                    //go back
                    _tempTrans.DOMove(kmno4.OriginPos, beakerResetTime);
                    tubeLiquid.DOColor(chemPink, chemDropIdleTime);
                    //shake tube next
                    ShakeTube();
                };
                //raise chemical level
                liquidMasks.DOScaleY(liquidLevels[curtLiquidIndex], chemDropIdleTime);
            };
        };
    }

    private void ShakeTube()
    {
        var _tubeTrans = tube.transform;
        _tubeTrans.DOMove(tubeShakePos, tubeLiftTime).onComplete += () =>
        {
            RotateTube(_tubeTrans);
        };
    }

    private void RotateTube(Transform _tubeTrans)
    {
        _tubeTrans.DORotate(tubeShakeRot, tubeShakeTime).SetEase(Ease.Linear).onComplete += () =>
        {
            _tubeTrans.DORotate(-tubeShakeRot, tubeShakeTime).SetEase(Ease.Linear).onComplete += () =>
            {
                curtShakeItteration++;
                if (curtShakeItteration >= tubeShakeItteration)
                {
                    //reset tube pos
                    //check Chemical Index
                    if(curtLiquidIndex <experimentEndIndex)
                    {
                        tubeLiquid.DOColor(tubeNormal, tubeShakeIdle);
                    }
                    _tubeTrans.DOMove(tubePos, tubeLiftTime).SetDelay(tubeShakeIdle).onComplete+=()=>
                    {
                        kmno4.EnableInteract();
                        if (curtLiquidIndex >= experimentEndIndex)
                        {
                            OnSimulationFinished?.Invoke();
                            kmno4.DisableInteract();
                            print("Simulation End");
                        }
                        //one chem cycle end
                    };
                }
                else
                {
                    //shake again
                    RotateTube(_tubeTrans);
                }
            };
        };
    }

    private void BurnerTapped()
    {
        burnerButt.enabled = false;
        flameObj.SetActive(true);
        kmno4.EnableInteract();
    }

    void TubePlaced()
    {
        tube.transform.DOMove(tubePos, tubePosTime).onComplete += () =>
        {
            burnerButt.GetComponent<Collider2D>().enabled = true;
            tube.DisableInteract();
        };
    }
    private void OnCorrectBeaker(DragManager _drag)
    {
        _drag.DisableInteract();
        PlayDropAnim(_drag.transform, true, _drag);
    }
    void PlayDropAnim(Transform _obj, bool _resetOnComplete = false, DragManager _drag = default)
    {
        _obj.DOMove(dropPos, beakerDropSpeed).onComplete += () =>
        {
            _obj.DORotate(dropRot, beakerRotSpeed).SetEase(Ease.Linear).onComplete +=
            () =>
            {
                //rotate object back
                _obj.DORotate(Vector3.zero, beakerRotSpeed).SetEase(Ease.Linear).
                SetDelay(beakerDropDuration).onComplete += () =>
                {
                    if (_resetOnComplete)
                    {
                        _drag.transform.DOMove(_drag.OriginPos, beakerResetTime);
                        CheckLiquidIndex();
                    }
                };
                //increase liquid level
                liquidMasks.DOScaleY(liquidLevels[curtLiquidIndex], beakerDropDuration);
            };
        };
    }
    void CheckLiquidIndex()
    {
        curtLiquidIndex++;
        switch (curtLiquidIndex)
        {
            case 1:
                beaker2.EnableInteract();
                break;
            case 2:
                tube.EnableInteract();
                break;
            default:
                break;
        }
    }
}
