using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoolingWateerSC : MonoBehaviour
{
    public string[] texts;
    public TMP_InputField[] obsFibs;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");
            texts[i] = texts[i].ToLower().Replace(" ", "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CLWaterSelfCheck()
    {
        if (obsFibs[0].text == texts[0])
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[1].text == texts[1])
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[2].text == texts[2])
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        /*for (int i = 0; i < obsFibs.Length; i++)
        {
            //scFibs[i].text = obsFibs[i].text;
            Debug.Log("For loop started.");
            if (obsFibs[i].text == texts[i])
            {
                //obsFibs[i].GetComponent<TMP_Text>().color = Color.green;
                obsFibs[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.green;
                Debug.Log("Ans at index is " + i + " is right");
            }
            else
            {
                *//*obsFibs[i].GetComponent<TMP_Text>().color = Color.red;*//*
                obsFibs[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.red;
                Debug.Log("Ans at index is " + i + " is wrong");
            }
        }*/
    }
}
