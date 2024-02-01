using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public InputField id_Inputfield;
    public InputField password_InputField;


    [Header("Sign Up")]
    public GameObject SignUpUi;




    public void ToggleSingUpUi()
    {
        if (SignUpUi.activeSelf)
        {
            SignUpUi.SetActive(false);
        }
        else
        {
            SignUpUi.SetActive(true);
        }
    }
}
