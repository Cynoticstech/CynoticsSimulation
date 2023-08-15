using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlassSlabSC : MonoBehaviour
{
    //public string[] texts;
    public TMP_InputField[] obsFibs;

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
        if (obsFibs[0].text == "incident")
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[1].text == "emergent")
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }

        if (obsFibs[2].text == "equal")
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
        }
        
    }
}
