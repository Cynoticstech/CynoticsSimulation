using DG.Tweening;
using UnityEngine;

public class HopesApparatus : MonoBehaviour
{
    [SerializeField] DragManager icePlate, saltPlate, waterBeaker;
    [SerializeField] GameObject IceObj, SaltObj;
    [SerializeField] Transform waterLevel, droppingWaterLevel, graphMask;
    [SerializeField]
    float plateDropSpeed = 1f, plateDropIdle = 1f, plateRotSpeed = 1f, plateResetSpeed = 3f,
        beakerDropSpeed = 1f, beakerRotSpeed = 1f, beakerIdleTime = 1f, maskTime = 10f, waterDropTime = 3f, waterRiseTime = 3f,
        waterRiseScale = 1f, waterDropScale = 1f;
    [SerializeField] Vector3 plateDropPos, plateRotPos, beakerDropPos, beakerDropRot;

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
                    () => curtStep++;
                };
            };
        };
    }
}
