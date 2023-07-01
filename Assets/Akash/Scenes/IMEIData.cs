using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
public static class IMEIData
{
    public static string GetIMEI()
    {
        string imei = string.Empty;

        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.BetinalPrivateLimited.Cynotics"))
        {
            using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                string telephonyServiceName = "phone";
                using (AndroidJavaObject telephonyManager = currentActivity.Call<AndroidJavaObject>("getSystemService", telephonyServiceName))
                {
                    imei = telephonyManager.Call<string>("getDeviceId");
                }
            }
        }

        return imei;
    }
}
#endif
