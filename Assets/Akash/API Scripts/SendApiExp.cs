using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendApiExp : MonoBehaviour
{
    public GameObject cockroach, amoebahydra;

    public Cockroach cockroachExp;
    public AmoebaAndHydra amh;

    public void SendOnActive()
    {
        if(cockroach.activeSelf == true)
        {
            cockroachExp.cockroachExpSend();
        }

        else if(amoebahydra.activeSelf == true)
        {
            amh.AmoebaHydraExpSend();
        }
    }
}
