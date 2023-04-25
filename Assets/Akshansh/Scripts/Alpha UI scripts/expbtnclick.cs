using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulations;


public class expbtnclick : MonoBehaviour
{
    public SimulationSetupManager.SimulationTypes buttType;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{
            SceneChangeScript.optionselect.selectsim = buttType;
            UnityEngine.SceneManagement.SceneManager.LoadScene("BiologySimulations"); 
            
        });
    }

    // Update is called once per frame
    
}
