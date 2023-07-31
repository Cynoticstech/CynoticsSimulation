using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObsRxnSC : MonoBehaviour
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

    public void ObsRxnSelfCheck()
    {
        // 1
        if (dropdown[0].value == 2)
        {
            dropdown[0].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[0].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 2
        if (dropdown[1].value == 6)
        {
            dropdown[1].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[1].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 3
        if (dropdown[2].value == 6)
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 4
        if (dropdown[3].value == 1)
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 5
        if (dropdown[4].value == 2)
        {
            dropdown[4].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[4].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 6
        if (dropdown[5].value == 3)
        {
            dropdown[5].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[5].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 7
        if (dropdown[6].value == 2)
        {
            dropdown[6].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[6].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 8
        if (dropdown[7].value == 4)
        {
            dropdown[7].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[7].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 9
        if (dropdown[8].value == 2)
        {
            dropdown[8].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[8].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 10
        if (dropdown[9].value == 2)
        {
            dropdown[9].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[9].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 11
        if (dropdown[10].value == 3)
        {
            dropdown[10].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[10].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 12
        if (dropdown[11].value == 3)
        {
            dropdown[11].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[11].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 13
        if (dropdown[12].value == 2)
        {
            dropdown[12].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[12].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 14
        if (dropdown[13].value == 2)
        {
            dropdown[13].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[13].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        // 15
        if (dropdown[14].value == 3)
        {
            dropdown[14].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[14].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }



        for (int i = 0; i < obsFibs.Length; i++)
        {
            //obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");

            if (obsFibs[0].text == "combination")
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 1 done
            if (obsFibs[1].text == "slaked")
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 2 done
            if (obsFibs[2].text == "sulphur dioxide")
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 3 done
            if (obsFibs[3].text == "sulphur trioxide")
            {
                obsFibs[3].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[3].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 4 done
            if (obsFibs[4].text == "reddish brown")
            {
                obsFibs[4].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[4].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 5 done
            if (obsFibs[5].text == "copper")
            {
                obsFibs[5].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[5].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 6 done
            if (obsFibs[6].text == "reddish brown")
            {
                obsFibs[6].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[6].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 7 done
            if (obsFibs[7].text == "single displacement" || obsFibs[7].text == "displacement")
            {
                obsFibs[7].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[7].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 8 done
            if (obsFibs[8].text == "precipitate" || obsFibs[8].text == "ppt")
            {
                obsFibs[8].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[8].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
            // 9 done
        }
    }
}
