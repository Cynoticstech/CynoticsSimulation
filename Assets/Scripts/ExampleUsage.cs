using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private PhonePePayment phonePePayment;

    void Start()
    {
        phonePePayment = GetComponent<PhonePePayment>();
    }

    public void OnPayButtonClicked()
    {
        float amount = 1.0f; // Example amount
        phonePePayment.InitiatePayment(amount);
    }
}