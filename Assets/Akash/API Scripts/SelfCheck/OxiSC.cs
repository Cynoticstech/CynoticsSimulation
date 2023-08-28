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

public class OxiSC : MonoBehaviour
{
   
    public TMP_InputField[] obsFibs;
    public TMP_Dropdown[] dropdown;
   
    private TextMeshProUGUI latestPressedButton;
    //public TMP_InputField[] scFibs;

    void Start()
    {
        
    }

    void Update()
    {

    }
    public void OxiSaveAns()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            DynamicDataHolder.Instance.OxiAddObsFibs.Add(obsFibs[i].text);
        }
        for (int i = 0; i < dropdown.Length; i++)
        {
            DynamicDataHolder.Instance.OxiAddropdown.Add(dropdown[i].value);
        }
    }
    public void OxiReloadAns()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = DynamicDataHolder.Instance.OxiAddObsFibs[i];
        }
        for (int i = 0; i < dropdown.Length; i++)
        {
            dropdown[i].value = DynamicDataHolder.Instance.OxiAddropdown[i];
        }
    }
    public void OxiSelfCheck()
    {
        if (dropdown[0].value == 1)
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
        if (dropdown[2].value == 1)
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[2].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }
        //
        if (dropdown[3].value == 2)
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.green;
        }
        else
        {
            dropdown[3].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
        }

        for (int i = 0; i < obsFibs.Length; i++)
        {
            obsFibs[i].text = obsFibs[i].text.ToLower().Replace(" ", "");

            if (obsFibs[0].text == "ethanoicacid")
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[0].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }

            if (obsFibs[1].text == "pink")
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[1].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }

            if (obsFibs[2].text == "addition")
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                obsFibs[2].transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>().color = Color.red;
            }
        }
    }
}

