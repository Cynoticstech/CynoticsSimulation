using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendApiExp : MonoBehaviour
{
    public GameObject cockroach, amoebahydra, mitosisMeiosis, Hibiscus, fishPegion, BioFertilizers, Microbes, HumanReproductiveSys, SuccessPopup;
    public TextMeshProUGUI text;

    public Cockroach cockroachExp;
    public AmoebaAndHydra amh;
    public MitosisandMeiosis mitoMis;
    public Hibiscus hibiscus;
    public FishPegion fishPeg;
    public BioFertilizer bioFertilizer;
    public Microbes microbes;
    public MaleAndFemale humanRepro;

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

        else if (mitosisMeiosis.activeSelf == true)
        {
            mitoMis.MitosisMeosisExpSend();
        }

        else if (Hibiscus.activeSelf == true)
        {
            hibiscus.HibiscusExpSend();
        }

        else if (fishPegion.activeSelf == true)
        {
            fishPeg.fpExpSend();
        }

        else if (BioFertilizers.activeSelf == true)
        {
            bioFertilizer.BioFExpSend();
        }

        else if (Microbes.activeSelf == true)
        {
            microbes.MicrobeExpSend();
        }

        else if (HumanReproductiveSys.activeSelf == true)
        {
            humanRepro.MFExpSend();
        }
    }

    public void SuccessAPISentPopup()
    {
        text.text = "Succesfully Sent To Cynotics";
        SuccessPopup.SetActive(true);
    }

    public void UnsuccessAPISentPopup()
    {
        text.text = "Try Again After Sometime";
        SuccessPopup.SetActive(true);
        
    }
}
