using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Simulations
{
    public class SimulationSceneManager : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void GoBack(string _scene = "Main Alpha Functionality Pages")
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene(_scene);
        }
    }
}
