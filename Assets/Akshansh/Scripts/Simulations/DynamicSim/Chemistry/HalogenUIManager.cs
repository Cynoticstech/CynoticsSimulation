using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class HalogenUIManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown[] InputDropDowns;
    [SerializeField] Transform tableContent,submitButt;
    [SerializeField] GameObject tableObj;
    [SerializeField] GameObject tableRowPref;
    public List<GameObject> ApiAnswers;

    public void ShowTable()
    {
        ApiAnswers.Clear();
        for(int i=0;i<tableContent.childCount-1;i++)
        {
            //exclude first and last obj
            if (i == 0)
                continue;
            Destroy(tableContent.GetChild(i).gameObject);
        }
        var _tempData = DynamicDataHolder.Instance.HalogeData;
        foreach (var v in _tempData)
        {
            var temp = Instantiate(tableRowPref, tableContent).transform;
            temp.GetChild(1).GetComponent<TMP_Text>().text = v.HalogenValues[0];
            temp.GetChild(2).GetComponent<TMP_Text>().text = v.HalogenValues[1];
            temp.GetChild(3).GetComponent<TMP_Text>().text = v.HalogenValues[2];
            temp.GetChild(4).GetComponent<TMP_Text>().text = v.HalogenValues[3];
            temp.GetChild(5).GetComponent<TMP_Text>().text = v.HalogenValues[5];
            temp.GetChild(6).GetComponent<TMP_Text>().text = v.HalogenValues[4];
            
        }
        submitButt.SetAsLastSibling();
        tableObj.SetActive(true);
    }
    public void LogData()
    {
        List<string> _data = new List<string>();
        foreach(var v in InputDropDowns)
        {
            _data.Add(v.captionText.text);
        }
        DynamicDataHolder.Instance.HalogeData.Add(new DynamicDataHolder.HalogenDataHolder
        {
            HalogenValues = _data.ToArray(),
        });
        ResetLogs();
    }
    public void ResetLogs()
    {
        foreach(var v in InputDropDowns)
        {
            v.value = 0;
        }
    }
}
