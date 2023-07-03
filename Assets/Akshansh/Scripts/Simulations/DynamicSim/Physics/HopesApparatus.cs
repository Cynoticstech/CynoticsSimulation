using DG.Tweening;
using UnityEngine;

public class HopesApparatus : MonoBehaviour
{
    [SerializeField] DragManager icePlate, saltPlate, waterBeaker;
    [SerializeField] GameObject IceObj, SaltObj;
    [SerializeField] Transform waterLevel, droppingWaterLevel, graphMask,t1,t2,beakerWater;
    [SerializeField]
    float plateDropSpeed = 1f, plateDropIdle = 1f, plateRotSpeed = 1f, plateResetSpeed = 3f,
        beakerDropSpeed = 1f, beakerRotSpeed = 1f, beakerIdleTime = 1f, maskTime = 10f, waterRiseTime = 3f,
        waterRiseScale = 1f, waterDropScale = 1f, expTime = 10f, t1Time = 4f, t2Time = 3f, t1Scale = 0.3f,t2Scale =0.2f,
        beakerWaterScale = 1.5f, graphScale = 0f;
    [SerializeField] Vector3 plateDropPos, plateRotPos, beakerDropPos, beakerDropRot;
    [SerializeField] SpriteRenderer[] SAltWaterSprites;
    [SerializeField] GameObject TimerScript;

    int curtStep = 0;

    private void Start()
    {
        icePlate.OnCorrectPlaced.AddListener(() =>
        {
            icePlate.DisableInteract();
            PlateAnim(icePlate.transform, true);
        });
        saltPlate.OnCorrectPlaced.AddListener(() =>
        {
            saltPlate.DisableInteract();
            PlateAnim(saltPlate.transform, false);
        });
        waterBeaker.OnCorrectPlaced.AddListener(() =>
        {
            waterBeaker.DisableInteract();
            var _tempTrans = waterBeaker.transform;
            _tempTrans.DOMove(beakerDropPos, beakerDropSpeed).onComplete+=()=>
            {
                _tempTrans.DORotate(beakerDropRot,beakerRotSpeed).onComplete+=()=>
                {
                    _tempTrans.DORotate(Vector3.zero,beakerRotSpeed).SetDelay(beakerIdleTime).onComplete+=()=>
                    {
                        _tempTrans.DOMove(waterBeaker.OriginPos, plateResetSpeed).onComplete += () =>
                        CheckStep();
                    };
                    waterLevel.DOScaleY(waterRiseScale, waterRiseTime);
                };
            };
        });
    }

    private void PlateAnim(Transform _temp, bool _isIce)
    {
        _temp.DOMove(plateDropPos, plateDropSpeed).onComplete += () =>
        {
            _temp.DORotate(plateRotPos, plateRotSpeed).SetEase(Ease.Linear).onComplete += () =>
            {
                _temp.DORotate(Vector3.zero, plateRotSpeed).SetEase(Ease.Linear).SetDelay(plateDropIdle).onComplete += () =>
                {
                    var _obj = _isIce ? IceObj : SaltObj;
                    _obj.SetActive(true);
                    _temp.DOMove(_isIce ? icePlate.OriginPos : saltPlate.OriginPos, plateResetSpeed).onComplete +=
                    () => CheckStep();
                };
            };
        };
    }

    void CheckStep()
    {
        curtStep++;
        if(curtStep>2)
        {
            //start anim
            print("Starting Exp");
            droppingWaterLevel.DOScaleX(waterDropScale,expTime);
            t1.DOScaleX(t1Scale, t1Time);
            t2.DOScaleX(t2Scale, t2Time);
            Color _col = Color.white;
            _col.a = 0;
            foreach(var v in SAltWaterSprites)
            {
                v.DOColor(_col, expTime);
                v.transform.DOScaleY(0.1f,expTime);
            }
            beakerWater.DOScaleX(beakerWaterScale, expTime);
            graphMask.DOScaleX(graphScale, expTime);
            TimerScript.SetActive(true);
        }
    }
}
