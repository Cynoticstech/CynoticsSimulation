using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    void Update()
    {
        // Check if the back button was pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            // Load the previous scene
            string previousScene = PlayerPrefs.GetString("previousScene");
            if (!string.IsNullOrEmpty(previousScene))
            {
                SceneManager.LoadScene(previousScene);
            }
        }
    }
}
