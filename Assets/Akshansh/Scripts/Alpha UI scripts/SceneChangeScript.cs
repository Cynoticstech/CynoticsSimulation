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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
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
