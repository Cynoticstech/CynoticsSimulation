using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReactivityManager : MonoBehaviour
{
    [SerializeField] Dropdown MetalType;
    [SerializeField] DragManager dropper;
    [SerializeField] Vector3[] dropPos, saltPos;
    [SerializeField]
    float dropperMoveTime = 1f, dropperOrgPosTime = 3f, saltFallTime = 2f,
        saltColorChangeDelay = 1f, saltColorChangeSpeed = 2f, waterColorChangeDelay = 1f,
        waterColorChangeSpeed = 2f;
    [SerializeField] Transform saltDropPos, saltPref;
    [SerializeField]
    Color[] WaterCol, SaltCol;
    [SerializeField] Color normalSaltCol, normalWaterCol;
    [SerializeField]
    SpriteRenderer[] waterRends;

    Collider2D dropperCol;
    Vector3 origDropperPos;

    private void Start()
    {
        dropper.OnCorrectPlaced.AddListener(CorrectPlacement);
        dropperCol = dropper.GetComponent<Collider2D>();
        origDropperPos = dropper.transform.position;
    }

    void CorrectPlacement()
    {
        switch (MetalType.value)
        {
            case 0:
                dropperCol.enabled = false;
                dropperCol.transform.DOMove(dropPos[0], dropperMoveTime).onComplete += () =>
                {
                    DropSalt(0); dropperCol.transform.DOMove(origDropperPos, dropperOrgPosTime).onComplete +=
                    () =>
                    {
                        dropperCol.enabled = true;
                    };
                };
                break;
            default:
                break;
        }
    }

    void DropSalt(int _index)
    {
        var _temp = Instantiate(saltPref, saltDropPos.transform.position, Quaternion.identity);
        _temp.transform.DOMove(saltPos[_index], saltFallTime);
        var _saltCol = _index switch
        {
            //return based on specific index for non reactive things
            _ => SaltCol[_index]
        };
        var _waterCols = _index switch
        {
            //return based on specific index for non reactive things
            _ => WaterCol[_index]
        };
        _temp.GetComponent<SpriteRenderer>().DOColor(_saltCol, saltColorChangeSpeed).SetDelay(saltColorChangeDelay);
        waterRends[_index].GetComponent<SpriteRenderer>().DOColor(_waterCols, waterColorChangeSpeed).SetDelay(waterColorChangeDelay);
    }
}
