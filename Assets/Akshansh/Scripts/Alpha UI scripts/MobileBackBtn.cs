using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileBackBtn : MonoBehaviour
{
    [SerializeReference]
    public GameObject SceneCheckRef;
    [SerializeReference]
    public GameObject HomePanel;
    [SerializeReference]
    public GameObject BioPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((SceneCheckRef.scene.name == "Simulations") && Input.GetKeyUp(KeyCode.Escape))
        {
            SceneCheckRef.GetComponent<navbtnclk>().alphaload();
        }
        if ((SceneCheckRef.scene.name == "Offline_Alpha_Pages") && Input.GetKeyUp(KeyCode.Escape))
        {
            if (HomePanel.activeSelf)
            {
                SceneCheckRef.GetComponent<navbtnclk>().splashload();
            }
            else if (BioPanel.activeSelf)
            {
                HomePanel.SetActive(true);
                BioPanel.SetActive(false);
            }

        }
        if ((SceneCheckRef.scene.name == "Splash Screen") && Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    
}
