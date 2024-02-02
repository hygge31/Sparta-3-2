using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public InputField id_Inputfield;
    public InputField password_InputField;

    public GameObject eye;
    public Sprite openEye;
    public Sprite closeEye;

    public Text message;

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


   public void ToggleShowPassward()
    {
        if(password_InputField.contentType == InputField.ContentType.Password)
        {
            password_InputField.contentType = InputField.ContentType.Standard;
            eye.GetComponent<Image>().sprite = openEye;
            password_InputField.ForceLabelUpdate();
        }
        else
        {
            password_InputField.contentType = InputField.ContentType.Password;
            eye.GetComponent<Image>().sprite = closeEye;
            password_InputField.ForceLabelUpdate();
        }
    }

    public void Login()
    {
        if (PlayerPrefs.HasKey(id_Inputfield.text))
        {
            string json = PlayerPrefs.GetString(id_Inputfield.text);
            UserData user = JsonUtility.FromJson<UserData>(json);
            if (DataManager.Instance.CheckPassward(user, password_InputField.text))
            {
                SceneManager.LoadScene("Task_1");
            }
            else
            {
                message.color = Color.red;
                message.text = "회원 정보가 일치하지 않습니다";
                StartCoroutine(MessageShowCo());
                
            }
        }
        else
        {
            message.color = Color.red;
            message.text = "회원 정보가 존재하지 않습니다";
            StartCoroutine(MessageShowCo());
        }
    }

    IEnumerator MessageShowCo()
    {
        yield return new WaitForSeconds(0.5f);
        message.text = "";
       
    }

}
