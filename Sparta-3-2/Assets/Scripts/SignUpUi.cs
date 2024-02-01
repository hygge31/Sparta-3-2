
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUi : MonoBehaviour
{
    public InputField id;
    public InputField _name;
    public InputField passward;
    public InputField passwardConfirm;

    public Sprite openEye;
    public Sprite closeEye;

    public GameObject passwardEye;
    public GameObject passwardConfirmEye;

    string idPattern = @"^[a-zA-Z0-9]{3,10}$";
    string namePattern = @"^[¤¡-¤¾°¡-ÆR]{2,5}$";
    string passwardPattern = @"^[a-zA-Z0-9]{5,15}$";


    public void IDRegexIsMatch()
    { 
        bool IsMatch = Regex.IsMatch(id.text,idPattern);
   
        if (IsMatch)
        {
            id.image.color = UnityEngine.Color.white;
            
        }
        else
        {
            id.image.color = UnityEngine.Color.red;
           
        }
    }

    public void IDCheck()
    {
        if(Regex.IsMatch(id.text, idPattern))
        {
            if (PlayerPrefs.HasKey(id.text))
            {
                UnityEngine.Debug.Log("¾ÆÀÌµð Á¸Àç");
            }
            else
            {
                UnityEngine.Debug.Log("¾ÆÀÌµð »ý¼º °¡´É");
            }
        }
        else
        {
            UnityEngine.Debug.Log("Àß¸øµÈ ¾ÆÀÌµð");
        }
    }


    public void NameRegexIsMatch()
    {
        bool IsMatch = Regex.IsMatch(_name.text, namePattern);

        if (IsMatch)
        {
            _name.image.color = UnityEngine.Color.white;
            
            
        }
        else
        {
            _name.image.color = UnityEngine.Color.red;
           

        }
    }
    public void PasswardRegexIsMatch()
    {
        bool IsMatch = Regex.IsMatch(passward.text, passwardPattern);

        if (IsMatch)
        {
            passward.image.color = UnityEngine.Color.white;

        }
        else
        {
            passward.image.color = UnityEngine.Color.red;

        }
    }

    public void ShowPassward()
    {
        if(passward.contentType == InputField.ContentType.Password)
        {
             passwardEye.GetComponent<Image>().sprite = openEye;
             passward.contentType = InputField.ContentType.Standard;
             passward.ForceLabelUpdate();
             
        }
        else
        {
            passwardEye.GetComponent<Image>().sprite = closeEye;
            passward.contentType = InputField.ContentType.Password;
            passward.ForceLabelUpdate();
        }
    }
    public void ShowPasswardConfirm()
    {
        if (passwardConfirm.contentType == InputField.ContentType.Password)
        {
            passwardConfirmEye.GetComponent<Image>().sprite = openEye;
            passwardConfirm.contentType = InputField.ContentType.Standard;
            passwardConfirm.ForceLabelUpdate();
        }
        else
        {
            passwardConfirmEye.GetComponent<Image>().sprite = closeEye;
            passwardConfirm.contentType = InputField.ContentType.Password;
            passwardConfirm.ForceLabelUpdate();
        }
    }

    public void PasswardIsMatch()
    {
        
        if (passward.text == passwardConfirm.text)
        {
            passwardConfirm.image.color = UnityEngine.Color.white;

        }
        else
        {
            passwardConfirm.image.color = UnityEngine.Color.red;

        }
    }




    public bool AllCheckIsMatch()
    {
        if(Regex.IsMatch(id.text, idPattern) && Regex.IsMatch(_name.text, namePattern) && Regex.IsMatch(passward.text, passwardPattern) && passward.text == passwardConfirm.text)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
