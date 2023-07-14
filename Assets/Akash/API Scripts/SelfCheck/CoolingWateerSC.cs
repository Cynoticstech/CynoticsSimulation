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
        for (int i = 0; i < obsFibs.Length; i++)
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
                /*obsFibs[i].GetComponent<TMP_Text>().color = Color.red;*/
                obsFibs[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>().color = Color.red;
                Debug.Log("Ans at index is " + i + " is wrong");
            }
        }
    }
}
