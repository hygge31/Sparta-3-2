using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Remit : DepositAndWithdrawal
{
    public InputField remitInputField;
    public Text remitMessage;
    public UserData remitUser;


  

    public void CheckRemitUser()
    {
        if (PlayerPrefs.HasKey(remitInputField.text))
        {
            string json = PlayerPrefs.GetString(remitInputField.text);
            UserData user = JsonUtility.FromJson<UserData>(json);
            remitUser = user;

            if (DataManager.Instance.user.userName == remitUser.userName)
            {
                remitMessage.text = $"�۱� ��� : {user.userName} (��) ";
            }
            else
            {
                remitMessage.text = $"�۱� ��� : {user.userName} ";
            }        
        }
        else
        {
            StartCoroutine(message("�۱� ����� �������� �ʽ��ϴ�."));
            remitUser = null;          
        }
    }



    public void InputRemit()
    {
        if(remitUser != null)
        {
            if (remitInputField.text == remitUser.userID)
            {
                int remitMoney = GetThousandCommaText(inputField.text);

                if (DataManager.Instance.user.bankAccount < remitMoney)
                {
                    StartCoroutine(message("�ܾ��� �����մϴ�."));
                }
                else
                {
                    if (remitUser.userID == DataManager.Instance.user.userID)
                    {

                    }
                    else
                    {
                        DataManager.Instance.user.bankAccount -= remitMoney;
                        remitUser.bankAccount += remitMoney;
                        UIManager.Instance.CallOnChangeBankAccountMoney();
                        DataManager.Instance.DataSave(remitUser, remitUser.userID);
                    }

                    OnReset();
                    StartCoroutine(message("�۱��� �Ϸ�Ǿ����ϴ�."));
                }
            }
            else
            {
                StartCoroutine(message("�۱� ����� ��ġ���� �ʽ��ϴ�."));
            }
        }
        else
        {
            StartCoroutine(message("�۱� ����� �����ϴ�."));
        }
        
    }


    public override void OnReset()
    {
        base.OnReset();
        remitUser = null;
        remitMessage.text = "";
        remitInputField.text = "";
    }

    IEnumerator message(string message)
    {
        remitMessage.text = message;
        yield return new WaitForSeconds(0.5f);
        remitMessage.text = "";
    }





}
