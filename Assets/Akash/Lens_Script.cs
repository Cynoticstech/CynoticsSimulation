using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Lens_Script : MonoBehaviour
{
    
    [SerializeField] float minScaleClamp, maxScaleClamp;
    [SerializeField] float min, max;
    [SerializeField] Slider slider;
    [SerializeField] Transform airScale;
    [SerializeField] Transform scale;
    [SerializeField] Vector3[] blurPos;
    [SerializeField] SpriteRenderer blurImg;
    [SerializeField] int currentActiveLens;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Material blurMat;
    [SerializeField] float correctVal, correctCheck = 0.1f;
    [SerializeField] float correctionVal = 0.1f;
    [SerializeField] float[] leftside, rightside;
    [SerializeField] List<GameObject> spawnedAns;
    [SerializeField] Transform tableL, tableR;
    [SerializeField] GameObject tablePrefab;
    [SerializeField] Transform contentorder;
    [SerializeField] float maxBlur;
 
    void Start()
    {
        slider.onValueChanged.AddListener(OnValChange);
        dropdown.onValueChanged.AddListener((value) => currentActiveLens = value);
    }

    public void OnValChange(float val)
    {
        airScale.transform.position = new Vector3(minScaleClamp + maxScaleClamp * val, airScale.transform.position.y, airScale.transform.position.z);
        scale.transform.position = new Vector3(min + max * val, scale.transform.position.y, scale.transform.position.z);
        float blurVal = Mathf.Abs(scale.transform.position.x - blurPos[currentActiveLens].x) * correctionVal;
        blurVal =  Mathf.Clamp(blurVal, 0, maxBlur);

        correctVal = blurVal;
        
        blurImg.color = new Color(blurImg.color.r, blurImg.color.g,blurImg.color.b, correctVal);
        //blurMat.SetFloat("_Size", correctVal);
    }

    public void logfl()
    {
        if( correctVal > correctCheck)
        {
            return;
        }
        foreach(var check in DynamicDataHolder.Instance.focalLengthL)
        {
            if (check == leftside[currentActiveLens])
                return;
        }
        DynamicDataHolder.Instance.focalLengthL.Add(leftside[currentActiveLens]);
        DynamicDataHolder.Instance.focalLengthR.Add(rightside[currentActiveLens]);
    }

    public void fillTables()
    {
        foreach(var v in spawnedAns)
        {
            Destroy(v);
        }
        spawnedAns.Clear();
        for( int i =0 ,z = DynamicDataHolder.Instance.focalLengthL.Count; i<DynamicDataHolder.Instance.focalLengthL.Count;i++,z--)
        {
            
            GameObject row = Instantiate(tablePrefab, contentorder);
            row.transform.SetSiblingIndex(tableL.GetSiblingIndex() + 1);
            row.transform.GetChild(0).GetComponent<TMP_Text>().text = (z ).ToString();
            row.transform.GetChild(1).GetComponent<TMP_Text>().text = "Tree";
            row.transform.GetChild(2).GetComponent<TMP_Text>().text = DynamicDataHolder.Instance.focalLengthL[i].ToString()+ " cm";
            spawnedAns.Add(row);
            row = Instantiate(tablePrefab, contentorder);
            row.transform.SetSiblingIndex(tableR.GetSiblingIndex() + 1);
            row.transform.GetChild(0).GetComponent<TMP_Text>().text = (z ).ToString();
            row.transform.GetChild(1).GetComponent<TMP_Text>().text = "Tree";
            row.transform.GetChild(2).GetComponent<TMP_Text>().text = DynamicDataHolder.Instance.focalLengthR[i].ToString()+ " cm";
            spawnedAns.Add(row);


        }
    }
    

    void Update()
    {
    }
}
