using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PhonePePayment : MonoBehaviour
{
    private string serverUrl = "http://localhost:3000";
    public bool PaymentSuccess { get; private set; }

    public IEnumerator InitiatePayment(float amount, string phoneNumber)
    {
        PaymentSuccess = false;
        string paymentUrl = $"{serverUrl}/pay?amount={amount}&phoneNumber={phoneNumber}";

        using (UnityWebRequest www = UnityWebRequest.Get(paymentUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error initiating payment: {www.error}");
            }
            else
            {
                var jsonResponse = www.downloadHandler.text;
                Debug.Log($"Payment initiation response: {jsonResponse}");

                PaymentResponse responseObj;
                try
                {
                    responseObj = JsonUtility.FromJson<PaymentResponse>(jsonResponse);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error parsing JSON response: {e.Message}");
                    yield break;
                }

                if (responseObj != null && !string.IsNullOrEmpty(responseObj.redirectUrl))
                {
                    Application.OpenURL(responseObj.redirectUrl);
                    Debug.Log($"Status URL: {serverUrl}/redirect-url/{responseObj.data.merchantTransactionId}");

                    using (UnityWebRequest statusRequest = UnityWebRequest.Get($"{serverUrl}/redirect-url/{responseObj.data.merchantTransactionId}"))
                    {
                        yield return statusRequest.SendWebRequest();

                        if (statusRequest.result == UnityWebRequest.Result.ConnectionError || statusRequest.result == UnityWebRequest.Result.ProtocolError)
                        {
                            Debug.LogError($"Error checking payment status: {statusRequest.error}");
                        }
                        else
                        {
                            var response = statusRequest.downloadHandler.text;
                            Debug.Log($"Payment status response: {response}");
                            if (response.Contains("PAYMENT_SUCCESS"))
                            {
                                PaymentSuccess = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("Redirect URL is null or empty.");
                }
            }
        }
    }

    [System.Serializable]
    private class PaymentResponse
    {
        public string redirectUrl;
        public PaymentData data;
    }

    [System.Serializable]
    private class PaymentData
    {
        public string merchantTransactionId;
    }
}
