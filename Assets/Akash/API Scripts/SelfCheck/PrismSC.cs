using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrismSC : MonoBehaviour
{
    //public string[] texts;
    //public TMP_InputField[] obsFibs;
    public TMP_InputField[] angR, angD;

    // Start is called before the first frame update
    void Start()
    {

        /*for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");
            //texts[i] = texts[i].ToLower().Replace(" ", "");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrismSelfCheck()
    {
       
//---------------------------x----------------------------x
        if(angR[0].text == "20.6")
        {
            angR[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angR[1].text == "30.67")
        {
            angR[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angR[2].text == "40.02")
        {
            angR[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angR[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        //-----------------------x-------------------x---------------------
        if (angD[0].text == "53.27")
        {
            angD[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angD[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angD[1].text == "39.26")
        {
            angD[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angD[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (angD[2].text == "40.45")
        {
            angD[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            angD[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        //-------------------------x----------------------x----------
    }
}
