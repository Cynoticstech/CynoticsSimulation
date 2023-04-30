using AkshanshKanojia.Controllers.ObjectManager;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BioDynamicMang : MonoBehaviour
{
    public UnityEvent OnSimFinished;
    public int CurtAnimIndex = 0;
    [SerializeField] Vector3[] solubilityAnimPos;
    [SerializeField] Animation[] solAnims;
    [SerializeField] string[] solAnimationName;
    [SerializeField] float[] solAnimDelays;
    [SerializeField] GameObject solBottle, NaObj,litmusObj;
    [SerializeField] ObjectController objCont;
    [SerializeField] Transform litmusTarget2;
    [SerializeField] float MoveSpeeed = 13f;

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
                break;
            case 1:
                solAnims[2].Play(solAnimationName[3]);
                yield return new WaitForSeconds(solAnimDelays[4]);
                solAnims[3].Play(solAnimationName[4]);
                yield return new WaitForSeconds(solAnimDelays[5]);
                OnSimFinished?.Invoke();
                break;
            case 2:
                solAnims[4].Play(solAnimationName[6]);
                yield return new WaitForSeconds(solAnimDelays[7]);
                solAnims[5].Play(solAnimationName[7]);
                yield return new WaitForSeconds(solAnimDelays[8]);
                OnSimFinished?.Invoke();
                break;
            default:
                yield return null;
                break;
        }
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
            if (CurtAnimIndex == 2 && litmusIndex != 0)
            {
                StartCoroutine(PlayAnimations(CurtAnimIndex));
            }
            else
            {
                StartCoroutine(LitmusDelay());
                curtTarget.GetComponent<DragManager>().targetLocation = litmusTarget2;
                litmusIndex++;
            }
        }
    }
}
