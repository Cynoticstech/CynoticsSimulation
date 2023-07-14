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

public class HopeSC : MonoBehaviour
{
    public string[] texts;
    public TMP_InputField[] obsFibs;
    public TMP_InputField[] scFibs;

    void Start()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");
            texts[i] = texts[i].ToLower().Replace(" ", "");
        }
    }

    void Update()
    {

    }

    public void HopeSelfCheck()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            //scFibs[i].text = obsFibs[i].text;
            Debug.Log("For loop started.");
            if (obsFibs[i].text == texts[i])
            {
                //obsFibs[i].GetComponent<TMP_Text>().color = Color.green;
                obsFibs[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
                Debug.Log("Ans at index is " + i + " is right");
            }
            else
            {
                /*obsFibs[i].GetComponent<TMP_Text>().color = Color.red;*/
                obsFibs[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
                Debug.Log("Ans at index is " + i + " is wrong");
            }
        }
    }
}

