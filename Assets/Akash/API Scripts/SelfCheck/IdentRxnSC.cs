using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IdentRxnSC : MonoBehaviour
{

    public TMP_InputField[] obsFibs;
    public TMP_Dropdown[] dropdown;

    private TextMeshProUGUI latestPressedButton;
    //public TMP_InputField[] scFibs;

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

    public void IdentRxnSelfCheck()
    {
        if (dropdown[0].value == 3)
        {
            dropdown[0].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[0].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[1].value == 1)
        {
            dropdown[1].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[1].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[2].value == 2)
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[3].value == 3)
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[4].value == 4)
        {
            dropdown[4].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[4].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[5].value == 2)
        {
            dropdown[5].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[5].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }

        for (int i = 0; i < obsFibs.Length; i++)
        {
            //obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");

            if (obsFibs[0].text == "2")
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 1 done
            if (obsFibs[1].text == "1")
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 2 done
            if (obsFibs[2].text == "2")
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 3 done
            if (obsFibs[3].text == "2")
            {
                obsFibs[3].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[3].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 4 done
            if (obsFibs[4].text == "1")
            {
                obsFibs[4].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[4].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 5 done
            if (obsFibs[5].text == "3")
            {
                obsFibs[5].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[5].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 6 done
        }
    }
}
