using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceticButtonManager : MonoBehaviour
{
    public GameObject[] AceticExps;

    public void SetExp(int _index)
    {
        foreach(var v in AceticExps)
        {
            v.SetActive(false);
        }
        AceticExps[_index].SetActive(true);
    }
}
