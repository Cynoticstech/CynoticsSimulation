using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BioFerti : MonoBehaviour
{
    public TMP_InputField[] fillups;
    public TMP_InputField[] showFillups;
    public TMP_InputField[] staticFillups;

    void Start()
    {
        for (int i = 0; i < fillups.Length; i++)
        {
            fillups[i].text = fillups[i].text.ToLower().Replace(" ", "");

        }
        for (int i = 0; i < showFillups.Length; i++)
        {
            showFillups[i].text = showFillups[i].text.ToLower().Replace(" ", "");

        }
        for (int i = 0; i < staticFillups.Length; i++)
        {
            staticFillups[i].text = staticFillups[i].text.ToLower().Replace(" ", "");

        }
    }

    void Update()
    {
        
    }

    public void BioFSelfCheck()
    {
        for (int i = 0; i < fillups.Length; i++)
        {
            showFillups[i].text = fillups[i].text;

            if (fillups[i].text == staticFillups[i].text)
            {
                showFillups[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                showFillups[i].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
        }

        
    }
}
