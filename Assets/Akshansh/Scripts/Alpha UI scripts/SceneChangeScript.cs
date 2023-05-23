using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using Simulations;

public class SceneChangeScript : MonoBehaviour
{
    public static SceneChangeScript optionselect;
    public SimulationSetupManager.SimulationTypes selectsim;

    void Awake()
    {
        print(gameObject.name);
        if(optionselect)
        {
            Destroy(gameObject); 
        }
        else
        {
            optionselect = this;
        }
        DontDestroyOnLoad(gameObject);
    }

  
}
