using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class DynamicDataHolder : MonoBehaviour
{
    [SerializeField] string SimSceneName;
    public static DynamicDataHolder Instance;
    public List<float> LoggedTime, LoggedTemp;
    public List<float> focalLengthL, focalLengthR;
    public List<float> meltT1, meltT2, Htime;
    public List<string> OxiAddObsFibs;
    public List<int> OxiAddropdown;

    

    //chemistry data
    [System.Serializable]
    public class ReactivityDataHolder
    {
        public int DropDownIndex;
        public int AnswerIndex;
    }
    public List<ReactivityDataHolder> ReactivityData;

    [System.Serializable]
    public struct HalogenDataHolder
    {
        public string[] HalogenValues;
    }
    public List<HalogenDataHolder> HalogeData;
    [System.Serializable]
    public struct OxiAddData
    {
       
    }
    public List<OxiAddData> OxiData;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (Instance == null)
        {
            return;
        }
        ReactivityData = new List<ReactivityDataHolder>();
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name != SimSceneName)
            {
                
                Destroy(gameObject);
            }
        };
    }
}
