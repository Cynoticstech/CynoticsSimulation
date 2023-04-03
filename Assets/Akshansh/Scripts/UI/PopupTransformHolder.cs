using UnityEngine;

public class PopupTransformHolder : MonoBehaviour
{
    /// <summary>
    /// Holds original position of panel
    /// </summary>
    Vector3 pos, rot, scale;
    private void Start()
    {
        pos = transform.position;
        rot = transform.eulerAngles;
        scale = transform.localScale;
    }

    private void OnDisable()
    {
        transform.position = pos;
        transform.eulerAngles = rot;
        transform.localScale = scale;
    }
}
