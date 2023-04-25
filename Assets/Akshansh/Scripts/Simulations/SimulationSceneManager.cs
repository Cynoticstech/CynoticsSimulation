using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Simulations
{
    public class SimulationSceneManager : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene("Main Alpha Functionality Pages");
        }
    }
}
