using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PhonePePayment : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000"; // Change this to your server URL

    public void InitiatePayment(float amount)
    {
        StartCoroutine(InitiatePaymentCoroutine(amount));
    }

    private IEnumerator InitiatePaymentCoroutine(float amount)
    {
        string paymentUrl = $"{serverUrl}/pay?amount={amount}";

        using (UnityWebRequest www = UnityWebRequest.Get(paymentUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error initiating payment: {www.error}");
            }
            else
            {
                Debug.Log("Payment initiation successful. Redirecting to PhonePe...");
                Application.OpenURL(www.url); // This will open the PhonePe payment page in the default browser
            }
        }
    }
}
