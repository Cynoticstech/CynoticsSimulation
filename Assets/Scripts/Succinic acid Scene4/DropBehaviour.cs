using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    public Vector3 destroyPosition; // Set your destroy position in the inspector
    public GameObject newPrefab; // Drag and drop your new prefab in the inspector

    // Update is called once per frame
    void Update()
    {
        // Check if the drop has reached the destroy position
        if (Vector3.Distance(transform.position, destroyPosition) < 0.1f)
        {
            // Replace the drop with a new prefab
            GameObject newDrop = Instantiate(newPrefab, transform.position, Quaternion.identity);

            // Destroy the current drop
            Destroy(gameObject);
        }
    }
}
