using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIClasses : MonoBehaviour
{
    [Serializable]
    public class SignUpDataHolder
    {
        public string email, password, phone, instituteId, username, dob;
    }

    [Serializable]
    public class LoginDataHolder
    {
        public string email, password;
    }
}


