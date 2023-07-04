using AkshanshKanojia.Controllers.ObjectManager;
using AkshanshKanojia.Inputs.Mobile;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DragManager : MobileInputs
{
    public UnityEvent OnCorrectPlaced;
    public UnityEvent OnIncorrectPlace;
    [HideInInspector] public Vector3 OriginPos{ get => originalPos; }

    [SerializeField] bool canInteract = true, useTapOffset = true, resetOnWrong = true;
    public Transform targetLocation;
    public Collider2D[] targetCols;
    [SerializeField] bool useTargetCol;
    [SerializeField] float validDist = 2f, zDepth = 10f,resetMoveSpeed =2f,origrEturntime =1.5f;
    [SerializeField] ObjectController objCont;
    [SerializeField] LayerMask raycastLayer;

    Vector3 tempOffset, tempStartPos,originalPos;

    bool isValid = false;
    Collider2D tempDropPos;

    public override void Start()
    {
        base.Start();
        objCont.OnMovementEnd += (obj) => { if (obj == gameObject) { EnableInteract(); } };
        originalPos = transform.position;
        OnIncorrectPlace.AddListener(() =>
        {
            MoveToTempStart();
        });
    }

    private void MoveToTempStart()
    {
        if (resetOnWrong)
        {
            DisableInteract();
            objCont.AddEvent(gameObject, tempStartPos, resetMoveSpeed,
                false);
            print("rEsetting pos");
        }
    }

    private void UpdatePos(MobileInputManager.TouchData _data)
    {
        var _temp = _data.TouchPosition;
        _temp.z = zDepth;
        _temp = Camera.main.ScreenToWorldPoint(_temp);
        _temp += tempOffset;
        transform.position = _temp;
    }

    public void DisableInteract()
    {
        canInteract = false;
    }
    public void EnableInteract()
    {
        canInteract = true;
    }
    public void ResetToOrignalPos()
    {
        transform.DOMove(originalPos, resetMoveSpeed);
    }
    public override void OnTapEnd(MobileInputManager.TouchData _data)
    {
        if (isValid)
        {
            isValid = false;
            if (!useTargetCol)
            {
                if (Vector3.Distance(transform.position, targetLocation.position) < validDist)
                {
                    OnCorrectPlaced?.Invoke();
                }
                else
                {
                    OnIncorrectPlace?.Invoke();
                }
            }
            else
            {
                foreach(var v in targetCols)
                {
                    if (v.bounds.Contains(transform.position))
                    {
                        tempDropPos = v;
                        OnCorrectPlaced?.Invoke();
                        return;
                    }
                }
                //if no valid colliders
                OnIncorrectPlace?.Invoke();
            }
        }
    }

    public override void OnTapMove(MobileInputManager.TouchData _data)
    {
        if (isValid)
        {
            UpdatePos(_data);
        }
    }

    public override void OnTapped(MobileInputManager.TouchData _data)
    {
        if (!canInteract)
            return;
        tempStartPos = transform.position;
        RaycastHit2D _hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(
            _data.TouchPosition), Mathf.Infinity, raycastLayer);
        if (_hit)
        {
            if (_hit.collider.gameObject == gameObject)
            {
                isValid = true;
                if (useTapOffset)
                {
                    var _temp = _data.TouchPosition;
                    _temp.z = zDepth;
                    _temp = Camera.main.ScreenToWorldPoint(_temp);
                    tempOffset = transform.position - _temp;
                }
                UpdatePos(_data);
            }
        }
    }

    public Collider2D GetTargetCol() => tempDropPos;

    public override void OnTapStay(MobileInputManager.TouchData _data)
    {
    }
}
