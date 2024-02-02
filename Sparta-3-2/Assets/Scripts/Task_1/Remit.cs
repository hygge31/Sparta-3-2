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
                remitMessage.text = $"송금 대상 : {user.userName} (나) ";
            }
            else
            {
                remitMessage.text = $"송금 대상 : {user.userName} ";
            }        
        }
        else
        {
            StartCoroutine(message("송금 대상이 존재하지 않습니다."));
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
                    StartCoroutine(message("잔액이 부족합니다."));
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
                    StartCoroutine(message("송금이 완료되었습니다."));
                }
            }
            else
            {
                StartCoroutine(message("송금 대상이 일치하지 않습니다."));
            }
        }
        else
        {
            StartCoroutine(message("송금 대상이 없습니다."));
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
