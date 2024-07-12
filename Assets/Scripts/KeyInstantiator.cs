using UnityEngine;
using UnityEngine.UI;

public class KeyInstantiator : MonoBehaviour
{
    public Button instantiateButton;
    public GameObject prefab;
    public GameObject targetGameObject;
     public Vector3 instantiatePosition;


    void Start()
    {
        // Add listener for the button click event
        instantiateButton.onClick.AddListener(OnButtonClick);

        // Initially set the button's interactable state based on the targetGameObject's active state
        instantiateButton.interactable = targetGameObject.activeSelf;
    }

    void Update()
    {
        // Continuously update the button's interactable state based on the targetGameObject's active state
        instantiateButton.interactable = targetGameObject.activeSelf;
    }

    void OnButtonClick()
    {
        if (targetGameObject.activeSelf)
        {
            Instantiate(prefab, instantiatePosition, Quaternion.identity);
        }
    }
}
