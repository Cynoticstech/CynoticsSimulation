using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExampleUsage : MonoBehaviour
{
    private PhonePePayment phonePePayment;
    private DatabaseReference reference;

    public GameObject physicsFreeGameObject;
    public GameObject chemistryFreeGameObject;
    public GameObject biologyFreeGameObject;

    public GameObject physicsPremiumGameObject;
    public GameObject chemistryPremiumGameObject;
    public GameObject biologyPremiumGameObject;

    //public GameObject gameObject1; // Pay page
    //public GameObject gameObject2; // Premium confirmation page

    private bool isPaidUser;

    async void Start()
    {
        phonePePayment = GetComponent<PhonePePayment>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        PlayerPrefs.SetString("UserId", "22eaef3cf1c3c31a0fe446d70c4de3e0e664dae2");
        PlayerPrefs.Save(); // Ensure it's saved
        await CheckPaymentStatus();
    }

    public async Task CheckPaymentStatus()
    {
        string userId = PlayerPrefs.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            Debug.Log("UserId is not set in PlayerPrefs");
            // Handle the case where userId is not set
        }
        else
        {
            Debug.Log($"UserId retrieved from PlayerPrefs: {userId}");
        }
        Debug.Log($"UserId retrieved from PlayerPrefs: {userId}");

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("UserId is null or empty.");
            isPaidUser = false;
            return;
        }

        DataSnapshot snapshot = await reference.Child("users").Child(userId).GetValueAsync();

        if (snapshot.Exists)
        {
            User user = JsonUtility.FromJson<User>(snapshot.GetRawJsonValue());
            Debug.Log($"User retrieved from database: {user.IsPaidUser}");
            isPaidUser = user.IsPaidUser;
        }
        else
        {
            Debug.LogError("User snapshot does not exist in the database.");
            isPaidUser = false; // Ensure isPaidUser is false if no snapshot exists
        }

        Debug.Log($"isPaidUser set to: {isPaidUser}");
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
            await InitiatePaymentAndUnlock(amount, phoneNumber, userId);
        }
    }

    private async Task InitiatePaymentAndUnlock(float amount, long phoneNumber, string userId)
    {
        await phonePePayment.InitiatePayment(amount, phoneNumber.ToString()).AsTask(this);

        if (phonePePayment.PaymentSuccess)
        {
            // Update the user's payment status in the database
            await reference.Child("users").Child(userId).Child("IsPaidUser").SetValueAsync(true);

            // Update the local payment status
            isPaidUser = true;

            Debug.Log("Premium content unlocked.");
        }
        else
        {
            Debug.LogError("Payment failed");
        }
    }
    public void OnSubjectButtonClicked(string subject)
    {
        if (isPaidUser)
        {
            switch (subject)
            {
                case "physics":
                    physicsPremiumGameObject.SetActive(true);
                    physicsFreeGameObject.SetActive(false);
                    break;
                case "chemistry":
                    chemistryPremiumGameObject.SetActive(true);
                    chemistryFreeGameObject.SetActive(false);
                    break;
                case "biology":
                    biologyPremiumGameObject.SetActive(true);
                    biologyFreeGameObject.SetActive(false);
                    break;
            }
        }
        else
        {
             switch (subject)
            {
                case "physics":
                    physicsFreeGameObject.SetActive(true);
                    physicsPremiumGameObject.SetActive(false);
                    break;
                case "chemistry":
                    chemistryFreeGameObject.SetActive(true);
                    chemistryPremiumGameObject.SetActive(false);
                    break;
                case "biology":
                    biologyFreeGameObject.SetActive(true);
                    biologyPremiumGameObject.SetActive(false);
                    break;
            }
        }
    }
    /*public void OnPayPageButtonClicked()
    {
        if (isPaidUser)
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(true);
        }
        else
        {
            gameObject1.SetActive(true);
            gameObject2.SetActive(false);
        }
    }*/
}