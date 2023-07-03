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
    [SerializeField] int currentActiveLens;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Material blurMat;
    [SerializeField] float correctVal;
    [SerializeField] float correctionVal = 0.1f;

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
        blurVal =  Mathf.Clamp(blurVal, 0, 1);

        correctVal = blurVal;

        blurMat.SetFloat("_Size", correctVal);
    }

    void Update()
    {
    }
}
