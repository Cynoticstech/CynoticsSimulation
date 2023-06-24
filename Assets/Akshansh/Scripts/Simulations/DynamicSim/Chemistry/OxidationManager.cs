using AkshanshKanojia.Inputs.Button;
using DG.Tweening;
using UnityEngine;

public class OxidationManager : MonoBehaviour
{
    [SerializeField] DragManager tube, beaker1, beaker2, kmno4;
    [SerializeField]
    float beakerDropSpeed = 1f, beakerRotSpeed = 2f, beakerResetTime = 4f, beakerDropDuration = 4f,
        tubePosTime = 1f;
    [SerializeField] int curtLiquidIndex = 0;
    [SerializeField] Transform liquidMasks;
    [SerializeField] GameObject flameObj;
    [SerializeField] ButtonInputManager burnerButt;

    [SerializeField]
    float[] liquidLevels;
    [SerializeField] Vector3 dropPos, dropRot, tubePos;

    private void Start()
    {
        AssigneEvents();
    }

    private void AssigneEvents()
    {
        beaker1.OnCorrectPlaced.AddListener(() => OnCorrectBeaker(beaker1));
        beaker2.OnCorrectPlaced.AddListener(() => OnCorrectBeaker(beaker2));
        tube.OnCorrectPlaced.AddListener(OnTubePlaced);
        burnerButt.OnTap += (obj) =>
        {
            OnBurnerTapped();
        };
        kmno4.OnCorrectPlaced.AddListener(OnChemicalDrop);
    }
    void OnChemicalDrop()
    {
        kmno4.DisableInteract();
        CheckLiquidIndex();

    }
    private void OnBurnerTapped()
    {
        burnerButt.enabled = false;
        flameObj.SetActive(true);
        kmno4.EnableInteract();
    }

    void OnTubePlaced()
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
