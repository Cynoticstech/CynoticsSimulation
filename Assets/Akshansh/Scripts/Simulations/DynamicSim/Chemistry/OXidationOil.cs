using DG.Tweening;
using UnityEngine;
using TMPro;

public class OXidationOil : MonoBehaviour
{
    [SerializeField] DragManager OilBeaker, IodineBeaker;
    [SerializeField]
    float beakerMOveTime = 1f, iodineMoveTime = 1f, beakerResetTime = 2f, IodineResetTime = 2f,
        beakeriDleTime = 1f, beakerRotTime = 1f,colorchangetime = 2f,tubeScale =1f;
    [SerializeField] Vector3 beakerPos, beakerRot, iodinePos, iodineRot;
    [SerializeField] Transform OilTrans, iodineTrans, tubelevel;
    [SerializeField] TMP_Dropdown selectionField;
    [SerializeField] SpriteRenderer tubeColor;
    [SerializeField] Color IodineColor;

    bool haveOil, haveIodine;

    private void Start()
    {
        tubeScale /= 2;
        OilBeaker.OnCorrectPlaced.AddListener(() =>
        {
            OilBeaker.DisableInteract();
            OilTrans.DOMove(beakerPos, beakerMOveTime).onComplete += () =>
              {
                  OilTrans.DORotate(beakerRot, beakerRotTime).SetEase(Ease.Linear).onComplete += () =>
                     {
                         tubelevel.DOScaleY(tubeScale,beakeriDleTime);
                         tubeScale *= 2;
                         OilTrans.DORotate(Vector3.zero, beakerRotTime).SetEase(Ease.Linear).SetDelay(beakeriDleTime).onComplete += () =>
                         {
                             OilTrans.DOMove(OilBeaker.OriginPos, beakerResetTime);
                             haveOil = true;
                             selectionField.interactable = false;
                             CheckStep();
                         };
                     };
              };
        });

        IodineBeaker.OnCorrectPlaced.AddListener(() =>
        {
            IodineBeaker.DisableInteract();
            iodineTrans.DOMove(iodinePos, iodineMoveTime).onComplete += () =>
            {
                iodineTrans.DORotate(iodineRot, beakerRotTime).SetEase(Ease.Linear).onComplete += () =>
                {
                    tubelevel.DOScaleY(tubeScale, beakeriDleTime);
                    tubeScale *= 2;

                    iodineTrans.DORotate(Vector3.zero, beakerRotTime).SetEase(Ease.Linear).SetDelay(beakeriDleTime).onComplete += () =>
                    {
                        iodineTrans.DOMove(IodineBeaker.OriginPos, IodineResetTime);
                        haveIodine = true;
                        CheckStep();
                    };
                };
            };
        });
    }

    void CheckStep()
    {
        if (haveIodine && haveOil)
        {
            print("Starting exp");
            if(selectionField.value == 3)//vanaspatee ghee index
            {
                tubeColor.DOColor(IodineColor,colorchangetime);
            }
        }
    }
}
