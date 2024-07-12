using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PhonePePayment : MonoBehaviour
{
    private string serverUrl = "https://beta.cynotics.in";
    public bool PaymentSuccess { get; private set; }

    private const float POLLING_INTERVAL = 5f; // Check every 5 seconds
    private const float TIMEOUT = 300f; // 5 minutes timeout

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
                    // Open the payment URL
                    Application.OpenURL(responseObj.redirectUrl);

                    // Start polling for payment status
                    yield return StartCoroutine(PollPaymentStatus(responseObj.data.merchantTransactionId));
                }
                else
                {
                    Debug.LogError("Redirect URL is null or empty.");
                }
            }
        }
    }

    private IEnumerator PollPaymentStatus(string merchantTransactionId)
    {
        float elapsedTime = 0f;

        while (elapsedTime < TIMEOUT)
        {
            using (UnityWebRequest statusRequest = UnityWebRequest.Get($"{serverUrl}/redirect-url/{merchantTransactionId}"))
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
                    PaymentStatusResponse statusResponse = JsonUtility.FromJson<PaymentStatusResponse>(response);
                    if (statusResponse.success && statusResponse.code == "PAYMENT_SUCCESS")
                    {
                        PaymentSuccess = true;
                        Debug.Log("Payment successful!");
                        Application.OpenURL("https://www.cynotics.in");
                        yield break; // Exit the coroutine if payment is successful
                    }
                }
            }

            yield return new WaitForSeconds(POLLING_INTERVAL);
            elapsedTime += POLLING_INTERVAL;
        }

        Debug.LogWarning("Payment status check timed out.");
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

    [System.Serializable]
    private class PaymentStatusResponse
    {
        public bool success;
        public string code;
        public string message;
        public PaymentStatusData data;
    }

    [System.Serializable]
    private class PaymentStatusData
    {
        public string merchantId;
        public string merchantTransactionId;
        public string transactionId;
        public int amount;
        public string state;
        public string responseCode;
        public PaymentInstrument paymentInstrument;
    }

    [System.Serializable]
    private class PaymentInstrument
    {
        public string type;
        public string utr;
        public string upiTransactionId;
        public string cardNetwork;
        public string accountType;
    }
}