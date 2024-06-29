using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResistorManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button resistor1;
    public Button resistor2;

    public GameObject prefabA;
    public GameObject prefabB;

    // Positions and rotations for 'single' option
    public Vector3 singlePositionA;
    public Vector3 singleRotationA;
    public Vector3 singlePositionB;
    public Vector3 singleRotationB;

    // Positions and rotations for 'series' option
    public Vector3 seriesPositionA;
    public Vector3 seriesRotationA;
    public Vector3 seriesPositionB;
    public Vector3 seriesRotationB;

    // Positions and rotations for 'parallel' option
    public Vector3 parallelPositionA;
    public Vector3 parallelRotationA;
    public Vector3 parallelPositionB;
    public Vector3 parallelRotationB;

    private GameObject currentA;
    private GameObject currentB;

    void Start()
    {
        // Set default dropdown option
        dropdown.value = 0;

        // Add listeners for the buttons
        resistor1.onClick.AddListener(OnResistor1Clicked);
        resistor2.onClick.AddListener(OnResistor2Clicked);

        // Add listener for dropdown value change
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int index)
    {
        // Destroy current GameObjects when dropdown value changes
        if (currentA != null) Destroy(currentA);
        if (currentB != null) Destroy(currentB);
    }

    void OnResistor1Clicked()
    {
        string option = dropdown.options[dropdown.value].text;

        switch (option)
        {
            case "Single":
                if (currentB != null) Destroy(currentB);
                InstantiateResistorA(singlePositionA, singleRotationA);
                break;
            case "Series":
                InstantiateResistorA(seriesPositionA, seriesRotationA);
                break;
            case "Parallel":
                InstantiateResistorA(parallelPositionA, parallelRotationA);
                break;
        }
    }

    void OnResistor2Clicked()
    {
        string option = dropdown.options[dropdown.value].text;

        switch (option)
        {
            case "Single":
                if (currentA != null) Destroy(currentA);
                InstantiateResistorB(singlePositionB, singleRotationB);
                break;
            case "Series":
                InstantiateResistorB(seriesPositionB, seriesRotationB);
                break;
            case "Parallel":
                InstantiateResistorB(parallelPositionB, parallelRotationB);
                break;
        }
    }

    void InstantiateResistorA(Vector3 position, Vector3 rotation)
    {
        if (currentA != null) Destroy(currentA);
        currentA = Instantiate(prefabA, position, Quaternion.Euler(rotation));
    }

    void InstantiateResistorB(Vector3 position, Vector3 rotation)
    {
        if (currentB != null) Destroy(currentB);
        currentB = Instantiate(prefabB, position, Quaternion.Euler(rotation));
    }
}
