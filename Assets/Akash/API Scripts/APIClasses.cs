using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIClasses : MonoBehaviour
{
    [Serializable]
    public class SignUpDataHolder
    {
        public string email, password, phone, instituteId, username, dob, deviceKey;
    }

    [Serializable]
    public class LoginDataHolder
    {
        public string email, password, deviceKey
;
    }

    [Serializable]
    public class OtpSend
    {
        public string OTP;
    }

    [Serializable]
    public class ForgotPassword
    {
        public string email;
    }

    [Serializable]
    public class ResetPassword
    {
        public string email, emailOtp, password;
    }
}


