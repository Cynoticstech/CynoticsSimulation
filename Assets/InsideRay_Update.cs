using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRay_Update : MonoBehaviour
{
    public LineRenderer InLine;
    public Transform leftPivot, rightPivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InLine.SetPosition(0, leftPivot.position);
        InLine.SetPosition(1, rightPivot.position);
    }
}
