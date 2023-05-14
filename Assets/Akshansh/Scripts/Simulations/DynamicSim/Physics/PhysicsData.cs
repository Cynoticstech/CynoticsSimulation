using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsData : MonoBehaviour
{
    [SerializeField] string SimSceneName;
    public static PhysicsData Instance;
    public List<float> LoggedTime, LoggedTemp;
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
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name != SimSceneName)
            {
                Destroy(gameObject);
            }
        };
    }
}
