using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class OpenPDF : MonoBehaviour
{
    // Attach this script to the button
    public Button yourButton;

    // Name of the PDF file in the StreamingAssets folder
    public string pdfFileName = "collision Mathematical calculations.pdf";

    void Start()
    {
        // Make sure to add a listener to the button
        yourButton.onClick.AddListener(OpenPdfFile);
    }

    public void OpenPdfFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, pdfFileName);

#if UNITY_ANDROID
        // On Android, you may need to handle the file path differently
        // Copy the file to a temporary location and open it from there
        StartCoroutine(OpenPDFOnAndroid(filePath));
#elif UNITY_IOS
        // On iOS, you may need to handle the file path differently
        // Application.OpenURL should work if the file is accessible
        Application.OpenURL(filePath);
#else
        // For PC and other platforms
        Application.OpenURL(filePath);
#endif
    }

    IEnumerator OpenPDFOnAndroid(string filePath)
    {
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();
    
        string tempPath = Path.Combine(Application.persistentDataPath, pdfFileName);
        File.WriteAllBytes(tempPath, www.downloadHandler.data);
    
        Application.OpenURL(tempPath);
    }
}
