using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Get_Exp_Data : MonoBehaviour
{
    public APIClasses.ExperimentData.DataSend[] test;

    public GameObject mitosisMeiosis, aceticAcid, Amoeba, BioFerti, ClBr, Cockroach, Cooling, EffectOfHeat, FishPegion, FocalLength, Hibiscus, HopesApp,
        magField, MaleFemale, microbes, oxiAdd, reactivityOfmetals, roleOfCO2;

    private void Start()
    {
        StartCoroutine(GetUserExperiment());
    }
    IEnumerator GetUserExperiment()
    {
        string userGuid = Get_Student_Details.guid;
        string baseUrl = "https://echo-admin-backend.vercel.app/api/experiments/";
        string url = baseUrl + userGuid;

        UnityWebRequest newRequest = UnityWebRequest.Get(url);
        yield return newRequest.SendWebRequest();

        if (newRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonText = newRequest.downloadHandler.text;
            //Debug.Log("Received JSON: " + jsonText);
            try
            {
                APIClasses.ExperimentResponse response = JsonUtility.FromJson<APIClasses.ExperimentResponse>(jsonText);
                test = response.data;
                Debug.Log("Exp Data Received");
            }
            catch (ArgumentException e)
            {
                Debug.LogError("JSON parsing error: " + e.Message);
            }
        }
        else
        {
            Debug.Log(newRequest.error);
        }
        Find();
    }

    public void Find()
    {
        foreach (APIClasses.ExperimentData.DataSend data in test)
        {
            if (data.experimentName == "Mitosis and meiosis")
            {
                Debug.Log("Found experiment data for Mitosis and meiosis");
                mitosisMeiosis.SetActive(true);
                
            }

            if (data.experimentName == "Focal length of convex lens")
            {
                Debug.Log("Found experiment data for convex lens");
                FocalLength.SetActive(true);
                
            }

            if (data.experimentName == "To identify chloride, bromide and iodide")
            {
                Debug.Log("Found experiment data for halogens");
                ClBr.SetActive(true);
            }

            if (data.experimentName == "Role of Carbon Dioxide")
            {
                Debug.Log("Role of Carbon Dioxide");
                roleOfCO2.SetActive(true);
            }

            if (data.experimentName == "Reactivity of metals")
            {
                Debug.Log("Reactivity of metals");
                reactivityOfmetals.SetActive(true);
            }

            if (data.experimentName == "Oxidation and Addition Reaction")
            {
                Debug.Log("Oxidation and Addition Reaction");
                oxiAdd.SetActive(true);
            }

            if (data.experimentName == "Industrial Microbes")
            {
                Debug.Log("Industrial Microbes");
                microbes.SetActive(true);
            }

            if (data.experimentName == "Male and Female")
            {
                Debug.Log("Male and Female");
                MaleFemale.SetActive(true);
            }

            if (data.experimentName == "Magnetic field due to electric current")
            {
                Debug.Log("Magnetic field due to electric current");
                magField.SetActive(true);
            }

            if (data.experimentName == "Hope's apparatus")
            {
                Debug.Log("Hope's apparatus");
                HopesApp.SetActive(true);
            }

            if (data.experimentName == "Hibiscus")
            {
                Debug.Log("Hibiscus");
                Hibiscus.SetActive(true);
            }

            if (data.experimentName == "Chordate animals")
            {
                Debug.Log("Chordate animals");
                FishPegion.SetActive(true);
            }

            if (data.experimentName == "Effect of heat on ice")
            {
                Debug.Log("Effect of heat on ice");
                EffectOfHeat.SetActive(true);
            }

            if (data.experimentName == "Temperature of hot water during natural cooling")
            {
                Debug.Log("Temperature of hot water during natural cooling");
                Cooling.SetActive(true);
            }

            if (data.experimentName == "Non-Chordate animals")
            {
                Debug.Log("Non-Chordate animals");
                Cockroach.SetActive(true);
            }

            if (data.experimentName == "Bio Fertilizers Microbes")
            {
                Debug.Log("Bio Fertilizers Microbes");
                BioFerti.SetActive(true);
            }

            if (data.experimentName == "Amoeba and Hydara")
            {
                Debug.Log("Amoeba and Hydara");
                Amoeba.SetActive(true);
            }

            if (data.experimentName == "Acetic Acid")
            {
                Debug.Log("Acetic Acid");
                aceticAcid.SetActive(true);
            }
        }
    }
    
}

