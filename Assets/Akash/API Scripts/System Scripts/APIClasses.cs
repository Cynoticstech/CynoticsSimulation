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
        public string email, password, deviceKey;

    }

    [Serializable]
    public class OtpSend
    {
        public string email;
        public string emailOTP;
    }

    [Serializable]
    public class ForgotPassword
    {
        public string email;
    }

    [Serializable]
    public class ResetPassword
    {
        public string email, emailOTP, password;
    }

    [Serializable]
    public class ApiResponse
    {
        public string status;
        public string message;
        public UserData user;
    }

    [Serializable]
    public class UserData
    {
        public string image;
        public string guid;
        public string username;
        public string dob;
        public string email;
        public string phone;
        public string instituteId;
        public string @class;
        public string physics;
        public string biology;
        public string chemistry;
        public string registrationDate;
        public string deviceKey;
        public string subsplan;
        public string currentKey;
        public InstituteData institute;
    }

    [Serializable]
    public class InstituteData
    {
        public string _id;
        public string guid;
        public string createdAt;
        public string displayId;
        public string name;
        public string registrationDate;
        public string lastTransactionDate;
        public string paymentStatus;
        public string numberOfKeys;
        public string numberOfStudents;
        public string numberOfTeachers;
        public string pincode;
        public string teachersInfo;
        public string adminusername;
        public string adminpancard;
        public string adminaadharcard;
        public string adminemail;
        public string adminphone;
        public string emailOtpInfo;
        public string phoneOtpInfo;
        public string isEmailVerified;
        public string isPhoneVerified;
        public string isLoginActive;
        public string type;
        public string password;
        public string image;
        public string slogan;
        public string organizationName;
        public string address;
        public string email;
        public string phone;
    }

    [System.Serializable]
    public class ExperimentData
    {
        [System.Serializable]
        public class DataSend
        {
            public string experimentName;
            public string guid;
            public long createdAt;
            public string moduleName;
            public string user;
            public Question[] questions;
            public string marks;
            public Comment comments;
        }

        [System.Serializable]
        public class Question
        {
            public string question;
            public string answer;
            public List<string> attemptedanswer;
        }

        [System.Serializable]
        public class Comment
        {
            public string name;
            public string comments;
        }
    }

    [System.Serializable]
    public class ExperimentResponse
    {
        public string status;
        public ExperimentData.DataSend[] data;
    }
}
