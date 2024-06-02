using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using System.Collections;

public class ExampleUsage : MonoBehaviour
{
    private PhonePePayment phonePePayment;
    private DatabaseReference reference;
    public GameObject unlockable1;
    public GameObject unlockable2;
    public GameObject unlockable3;
    public GameObject lockable1;
    public GameObject lockable2;
    public GameObject lockable3;

    void Start()
    {
        phonePePayment = GetComponent<PhonePePayment>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void OnPayButtonClicked()
    {
        string userId = PlayerPrefs.GetString("UserId");
        DataSnapshot snapshot = await reference.Child("users").Child(userId).GetValueAsync();

        if (snapshot.Exists)
        {
            User user = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
            long phoneNumber = user.PhoneNumber;
            float amount = 1.0f; // Example amount
            StartCoroutine(InitiatePaymentAndUnlock(amount, phoneNumber, userId));
        }
    }

    private IEnumerator InitiatePaymentAndUnlock(float amount, long phoneNumber, string userId)
    {
        yield return StartCoroutine(phonePePayment.InitiatePayment(amount, phoneNumber.ToString()));

        if (phonePePayment.PaymentSuccess)
        {
            lockable1.SetActive(false);
            lockable2.SetActive(false);
            lockable3.SetActive(false);
            unlockable1.SetActive(true);
            unlockable2.SetActive(true);
            unlockable3.SetActive(true);
        }
        else
        {
            Debug.LogError("Payment failed");
        }
    }
}