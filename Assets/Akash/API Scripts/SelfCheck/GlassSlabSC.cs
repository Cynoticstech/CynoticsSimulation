using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassSlabSC : MonoBehaviour
{
    //public string[] texts;
    public TMP_InputField[] obsFibs;
    public TMP_InputField[] angR, angE;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");
            //texts[i] = texts[i].ToLower().Replace(" ", "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlSlabSelfCheck()
    {
        if (obsFibs[0].text.ToLower().Replace(" ", "") == "incident")
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[1].text.ToLower().Replace(" ", "") == "emergent")
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[2].text.ToLower().Replace(" ", "") == "equal")
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
//---------------------------x----------------------------x
        if(angR[0].text == "20.6")
        {
            angR[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angR[1].text == "29.1")
        {
            angR[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angR[2].text == "35.6")
        {
            angR[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        //-----------------------x-------------------x---------------------
        if (angE[0].text == "30")
        {
            angE[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angE[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angE[1].text == "45")
        {
            angE[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angE[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angE[2].text == "60")
        {
            angE[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angE[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        //-------------------------x----------------------x----------
    }
}
