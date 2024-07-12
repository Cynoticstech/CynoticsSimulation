using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphSceneLoad : MonoBehaviour
{
    // Name of the scene to load
    private string GraphSceneName = "Graph";
    private string alphaFunctionalityPagesSceneName = "Main Alpha Functionality Pages";

    // Update is called once per frame
    void Update()
    {
        // Check if the Back button was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.orientation = ScreenOrientation.Portrait;
            // If the Back button is pressed, load the Alpha Functionality Pages scene
            SceneManager.LoadScene(alphaFunctionalityPagesSceneName);
        }
    }

    public void OnButtonClick()
    {
        // Load the Momentum scene when the button is clicked
        SceneManager.LoadScene(GraphSceneName);
    }

    public void OnButtonClick2()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        // Load the Momentum scene when the button is clicked
        SceneManager.LoadScene(alphaFunctionalityPagesSceneName);
    }
}
