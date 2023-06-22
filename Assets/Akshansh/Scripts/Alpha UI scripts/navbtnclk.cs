using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class navbtnclk : MonoBehaviour
{
    //public enum Btnlist {logout, loginsubmit, signsubmit, studentselect, inredirect, upredirect}
    //public Btnlist btl;
    //public Button logout_btn, logsub_btn, sisub_btn, stdntlog_btn, inredirect_btn, upredirect_btn;
    //public GameObject parentobj, childobj;
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
       
        
       
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Currently Active Scene is " + scene.name);
    }
   
    public void splashload()
    {
        SceneManager.LoadScene("Splash Screen");
    }
    public void alphainactive()
    {
        //SceneManager.Set//
    }
    public void studentloginload()
    {
        //SceneManager.LoadScene("Student Login");
        SceneManager.LoadScene("Offline_Alpha_Pages");
    }
    public void studentSignUpload()
    {
        //SceneManager.LoadScene("Student Sign up 1");
        SceneManager.LoadScene("Offline_Alpha_Pages");
    }
    public void alphaload()
    {
        SceneManager.LoadScene("Offline_Alpha_Pages");
    }

}
