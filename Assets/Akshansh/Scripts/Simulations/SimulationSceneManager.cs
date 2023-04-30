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
        public void GoBack()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("Main Alpha Functionality Pages");
        }
    }
}
