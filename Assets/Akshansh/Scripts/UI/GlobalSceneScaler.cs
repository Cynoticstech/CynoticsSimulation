using UnityEngine;

public class GlobalSceneScaler : MonoBehaviour
{
    [SerializeField] GameObject SimHolder;
    [SerializeField] Vector3[] Scales;
    void Start()
    {
        if (Camera.main.aspect >= 1.7)
        {
            Debug.Log("16:9");
            SimHolder.transform.localScale = Scales[0];
        }
        else if (Camera.main.aspect >= 1.5)
        {
            Debug.Log("3:2");
            SimHolder.transform.localScale = Scales[1];

        }
        else
        {
            Debug.Log("4:3");
            SimHolder.transform.localScale = Scales[2];

        }
    }
}
