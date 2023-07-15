/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeltingIceSC : MonoBehaviour
{
    public string Text1;
    public TMP_InputField ObsFib1;
    public TMP_InputField SCFib1;

    void Start()
    {
        ObsFib1.text.ToLower();
        Text1.ToLower();
        ObsFib1.text.Replace(" ", "");
        Text1.Replace(" ", "");
    }

    void Update()
    {
        
    }
    public void IceSelfCheck()
    {
        SCFib1.text = ObsFib1.text;

        if(ObsFib1.text == Text1)
        {
            SCFib1.GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            SCFib1.GetComponent<TMP_Text>().color = Color.red;
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FLConvexSC : MonoBehaviour
{
    
    public TMP_InputField[] obsFibs;
    void Start()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");
            
        }
    }

    void Update()
    {

    }

    public void FLConvexSelfCheck()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            if ((obsFibs[0].text == "16" && obsFibs[1].text == "16") || (obsFibs[0].text == "14" && obsFibs[1].text == "14") || (obsFibs[0].text == "12" && obsFibs[1].text == "12"))
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }

           


            /* //scFibs[i].text = obsFibs[i].text;
             Debug.Log("For loop started.");
             if (obsFibs[i].text == texts[i])
             {
                 //obsFibs[i].GetComponent<TMP_Text>().color = Color.green;
                 obsFibs[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
                 Debug.Log("Ans at index is " + i + " is right");
             }
             else
             {
                 *//*obsFibs[i].GetComponent<TMP_Text>().color = Color.red;*//*
                 obsFibs[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
                 Debug.Log("Ans at index is " + i + " is wrong");
             }*/

        }
    }
}

