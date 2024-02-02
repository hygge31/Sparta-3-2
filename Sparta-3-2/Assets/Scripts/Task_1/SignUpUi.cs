
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class SignUpUi : MonoBehaviour
{
    public InputField id;
    public InputField _name;
    public UnityEngine.UI.Text message;
    public InputField passward;
    public InputField passwardConfirm;

    public Sprite openEye;
    public Sprite closeEye;

    public GameObject passwardEye;
    public GameObject passwardConfirmEye;

    public UnityEngine.UI.Text wrongMessage;
    public GameObject suceseMessage;
    string idPattern = @"^[a-zA-Z0-9]{3,10}$";
    string namePattern = @"^[¤¡-¤¾°¡-ÆR]{2,5}$";
    string passwardPattern = @"^[a-zA-Z0-9]{5,15}$";


    public void IDRegexIsMatch()
    { 
        bool IsMatch = Regex.IsMatch(id.text,idPattern);
        message.text = "";
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
                message.color = UnityEngine.Color.red;
                message.text = "¾ÆÀÌµð°¡ ÀÌ¹Ì Á¸ÀçÇÕ´Ï´Ù.";
            }
            else
            {
                message.color = UnityEngine.Color.white;
                message.text = "À¯È¿ÇÑ ¾ÆÀÌµð ÀÔ´Ï´Ù.";
            }
        }
        else
        {
            message.color = UnityEngine.Color.red;
            message.text = "Àß¸øµÈ ¾ÆÀÌµð ÀÔ´Ï´Ù.";
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
             passwardEye.GetComponent<UnityEngine.UI.Image>().sprite = openEye;
             passward.contentType = InputField.ContentType.Standard;
             passward.ForceLabelUpdate();
             
        }
        else
        {
            passwardEye.GetComponent<UnityEngine.UI.Image>().sprite = closeEye;
            passward.contentType = InputField.ContentType.Password;
            passward.ForceLabelUpdate();
        }
    }
    public void ShowPasswardConfirm()
    {
        if (passwardConfirm.contentType == InputField.ContentType.Password)
        {
            passwardConfirmEye.GetComponent<UnityEngine.UI.Image>().sprite = openEye;
            passwardConfirm.contentType = InputField.ContentType.Standard;
            passwardConfirm.ForceLabelUpdate();
        }
        else
        {
            passwardConfirmEye.GetComponent<UnityEngine.UI.Image>().sprite = closeEye;
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

    public void ResetSignUpUi()
    {
        
        id.text = "";
        _name.text = "";
        passward.text = "";
        passwardConfirm.text = "";
        id.image.color = UnityEngine.Color.white;
        _name.image.color = UnityEngine.Color.white;
        passward.image.color = UnityEngine.Color.white;
        passwardConfirm.image.color = UnityEngine.Color.white;
        gameObject.SetActive(false);
    }
    
    public void SingUp()
    {
        if (AllCheckIsMatch())
        {
            DataManager.Instance.DataSave(id.text, _name.text, passward.text);
            suceseMessage.SetActive(true);

        }
        else
        {
            StartCoroutine(DelayMessageCo());
        }
    }

    IEnumerator DelayMessageCo()
    {
        wrongMessage.text = "Á¤º¸¸¦ Á¦´ë·Î ÀÔ·ÂÇØ ÁÖ¼¼¿ä";
        yield return new WaitForSeconds(0.5f);
        wrongMessage.text = "";
    }

    public void CloseSuccessMessage()
    {
        suceseMessage.SetActive(false);
        ResetSignUpUi();

    }

}
