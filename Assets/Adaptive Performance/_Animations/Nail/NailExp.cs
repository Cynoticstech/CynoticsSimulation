using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailExp : MonoBehaviour
{
    public GameObject NailLeft;
    public GameObject NailRight;

    public GameObject NailA1;
    public GameObject NailA2;

    public GameObject TubeR;
    public GameObject TubeL;

    public void NailExpRL()
    {
        if(NailLeft.transform.position.x < 0 )
        {
            TubeR.GetComponent<Animator>().enabled = true;
            NailA1.SetActive(true);
        }
        if (NailLeft.transform.position.x > 0)
        {
            TubeL.GetComponent<Animator>().enabled = true;
            NailA2.SetActive(true);
        }
        NailRight.GetComponent<DragManager>().enabled = false;
        NailLeft.GetComponent<DragManager>().enabled = false;
    }

    public void NailExpLR()
    {
        if (NailRight.transform.position.x < 0)
        {
            TubeR.GetComponent<Animator>().enabled = true;
            NailA1.SetActive(true);
        }
        else
        {
            TubeL.GetComponent<Animator>().enabled = true;
            NailA2.SetActive(true);
        }
        NailRight.GetComponent<DragManager>().enabled = false;
        NailLeft.GetComponent<DragManager>().enabled = false;
    }
}
