using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimDropdownMang : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropDown;
    [SerializeField] Sprite[] spritesToShow;
    [SerializeField] Image TargetPlaceholder;

    private void Start()
    {
        dropDown.onValueChanged.AddListener(OnChange);
    }
    void OnChange(int _index)
    {
        TargetPlaceholder.sprite = spritesToShow[dropDown.value];
    }
}
