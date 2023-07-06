using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirect_Url : MonoBehaviour
{
    public void Redirect(string _url)
    {
        Application.OpenURL(_url);
    }
}
