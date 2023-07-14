using System.Collections;
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
}

/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeltingIceSC : MonoBehaviour
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

    public void IceSelfCheck()
    {
        for (int i = 0; i < obsFibs.Length; i++)
        {
            scFibs[i].text = obsFibs[i].text;

            if (obsFibs[i].text == texts[i])
            {
                scFibs[i].GetComponent<TMP_Text>().color = Color.green;
            }
            else
            {
                scFibs[i].GetComponent<TMP_Text>().color = Color.red;
            }
        }
    }
}*/

