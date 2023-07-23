using AkshanshKanojia.Controllers.ObjectManager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BioDynamicMang : MonoBehaviour
{
    public UnityEvent OnSimFinished;
    public int CurtAnimIndex = 0;

    [SerializeField] TMP_Text InterfaceTxt;
    [SerializeField] Vector3[] solubilityAnimPos;
    [SerializeField] Animation[] solAnims;
    [SerializeField] string[] solAnimationName;
    [SerializeField] float[] solAnimDelays;
    [SerializeField] GameObject solBottle, NaObj, litmusObj;
    [SerializeField] ObjectController objCont;
    [SerializeField] Transform litmusTarget2;
    [SerializeField] float MoveSpeeed = 13f;
    [SerializeField] Vector3 dropperLevelZeroScale;

    [SerializeField] Vector3 dropperLevelZeroPos;
    [SerializeField] Vector3 dropperLevelFullScale;

    [SerializeField] Vector3 dropperLevelFullPos;
    [SerializeField] Transform dropperLiquidInsideMask;
    [SerializeField] float animationDuration = 2f;
    [SerializeField] float elapsedTime = 0f;
    [SerializeField] bool isScaling = false;
    [SerializeField] bool hasSucked = false;

    [SerializeField] string[] InterfaceBodies;
    int litmusIndex = 0;
    GameObject curtTarget;
    private void Start()
    {
        objCont.OnMovementEnd += OnReached;
    }

    IEnumerator PlayAnimations(int _type)
    {
        switch (_type)
        {
            case 0:
                yield return new WaitForSeconds(solAnimDelays[0]);
                solAnims[0].Play(solAnimationName[0]);
                yield return new WaitForSeconds(solAnimDelays[1]);
                solAnims[1].Play(solAnimationName[1]);
                yield return new WaitForSeconds(solAnimDelays[2]);
                solAnims[1].Play(solAnimationName[2]);
                yield return new WaitForSeconds(solAnimDelays[3]);
                OnSimFinished?.Invoke();
                InterfaceTxt.text = InterfaceBodies[0];
                break;
            case 1:
                solAnims[2].Play(solAnimationName[3]);
                yield return new WaitForSeconds(solAnimDelays[4]);
                solAnims[3].Play(solAnimationName[4]);
                yield return new WaitForSeconds(solAnimDelays[5]);
                OnSimFinished?.Invoke();
                InterfaceTxt.text = InterfaceBodies[1];
                break;
            case 2:
                solAnims[4].Play(solAnimationName[6]);
                yield return new WaitForSeconds(solAnimDelays[7]);
                solAnims[5].Play(solAnimationName[7]);
                yield return new WaitForSeconds(solAnimDelays[8]);
                OnSimFinished?.Invoke();
                InterfaceTxt.text = InterfaceBodies[2];
                break;
            default:
                yield return null;
                break;
        }
    }
    public void Pungent()
    {
        InterfaceTxt.text = InterfaceBodies[3];

    }
    public void PlaySolutionAnim()
    {
        curtTarget = solBottle;
        objCont.AddEvent(curtTarget, solubilityAnimPos[0], MoveSpeeed, true);
        CurtAnimIndex = 0;
    }
    public void PlayNaAnim()
    {
        curtTarget = NaObj;
        objCont.AddEvent(curtTarget, solubilityAnimPos[1], MoveSpeeed, true);
        CurtAnimIndex = 1;
    }
    public void dropperLiquidSuck()
    {
        StartCoroutine(ScaleOverTimeInc());
    }
    public void dropperLiquidSquirt()
    {
        StartCoroutine(ScaleOverTimeDec());
    }

    public void SuckOrSquirt()
    {
        if(!hasSucked)
        {
            dropperLiquidSuck();
        }
        else
        {
            dropperLiquidSquirt();
        }
    }
    public IEnumerator ScaleOverTimeInc()
    {
        elapsedTime = 0f;
        isScaling = true;
        Vector3 startScale = dropperLiquidInsideMask.localScale;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            dropperLiquidInsideMask.localScale = Vector3.Lerp(dropperLevelZeroScale, dropperLevelFullScale, elapsedTime / animationDuration);
           
            dropperLiquidInsideMask.localPosition = Vector3.Lerp(dropperLevelZeroPos, dropperLevelFullPos, elapsedTime / animationDuration);
            yield return null;
        }
        dropperLiquidInsideMask.localScale = dropperLevelFullScale; // Ensure reaching the exact target scale
        dropperLiquidInsideMask.localScale = dropperLevelFullPos;
        isScaling = false;
        hasSucked = true;
    }
    public IEnumerator ScaleOverTimeDec()
    {
        elapsedTime = 0f;
        isScaling = true;
        Vector3 startScale = dropperLiquidInsideMask.localScale;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            dropperLiquidInsideMask.localScale = Vector3.Lerp(dropperLevelFullScale, dropperLevelZeroScale, elapsedTime / animationDuration);
            dropperLiquidInsideMask.localPosition = Vector3.Lerp(dropperLevelFullPos, dropperLevelZeroPos, elapsedTime / animationDuration);
            yield return null;
        }
        dropperLiquidInsideMask.localScale = dropperLevelZeroScale; // Ensure reaching the exact target scale
        dropperLiquidInsideMask.localScale = dropperLevelZeroPos;
        isScaling = false;
    }
    public void CheckLitmus()
    {
        CurtAnimIndex = 2;
        if (litmusIndex == 0)
        {
            curtTarget = litmusObj;
            objCont.AddEvent(curtTarget, solubilityAnimPos[2], MoveSpeeed, true);
        }
        else
        {
            objCont.AddEvent(curtTarget, solubilityAnimPos[3], MoveSpeeed, true);
        }
    }
    IEnumerator LitmusDelay()
    {
        solAnims[4].Play(solAnimationName[5]);
        yield return new WaitForSeconds(solAnimDelays[6]);
        curtTarget.GetComponent<BoxCollider2D>().enabled = true;
    }
    void OnReached(GameObject obj)
    {
        if (obj == curtTarget)
        {
            if (CurtAnimIndex == 2 && litmusIndex == 0)

            {
                StartCoroutine(LitmusDelay());
                curtTarget.GetComponent<DragManager>().targetLocation = litmusTarget2;
                litmusIndex++;
                return;
            }
            StartCoroutine(PlayAnimations(CurtAnimIndex));
        }
    }
}
