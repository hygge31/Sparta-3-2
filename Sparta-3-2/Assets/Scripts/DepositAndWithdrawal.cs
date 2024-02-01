using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    DEPOSIT,
    WITHDRAWAL
}

public class DepositAndWithdrawal : Util
{
    public InputField inputField;
    public Type eType;
    public void TenThousandBtn()
    {
        int num = GetThousandCommaText(inputField.text);
        inputField.text = GetThousandCommaText(num + 10000);
    }
    public void ThirtyThousandBtn()
    {
        int num = GetThousandCommaText(inputField.text);
        inputField.text = GetThousandCommaText(num + 30000);
    }
    public void FiftyThousandBtn()
    {
        int num = GetThousandCommaText(inputField.text);
        inputField.text = GetThousandCommaText(num + 50000);
    }

    public void InputBtn()
    {
        int number = GetThousandCommaText(inputField.text);
        int curBankAccount = DataManager.Instance.user.bankAccount;
        int curMyWallet = DataManager.Instance.user.myWallet;

        if (eType == Type.WITHDRAWAL) //출금
        {
           if(curBankAccount < number)
            {
                UIManager.Instance.ShowInsufficientBalanceMessage();
                inputField.text = "";
            }
            else
            {
                DataManager.Instance.user.bankAccount -= number;
                DataManager.Instance.user.myWallet += number;
                UIManager.Instance.CallOnChangeBankAccountMoney();
                inputField.text = "";
            }
        }
        else //입금
        {
            if (curMyWallet < number)
            {
                UIManager.Instance.ShowInsufficientBalanceMessage();
                inputField.text = "";
            }
            else
            {
                DataManager.Instance.user.bankAccount += number;
                DataManager.Instance.user.myWallet -= number;
                UIManager.Instance.CallOnChangeBankAccountMoney();
                inputField.text = "";
            }
        }
        
    }

    public void InputAll()
    {
       
        int curBankAccount = DataManager.Instance.user.bankAccount;
        int curMyWallet = DataManager.Instance.user.myWallet;

        if (eType == Type.WITHDRAWAL) //출금
        {
            inputField.text = GetThousandCommaText(curBankAccount);
            
        }
        else //입금
        {
            inputField.text = GetThousandCommaText(curMyWallet);
        }
    }

    public void ChangeInputField()
    {
        string curText = inputField.text;
        if(int.TryParse(inputField.text.Replace(",",""),out int number))
        {
            inputField.text = GetThousandCommaText(number);
        }
        else
        {
            string chr = "";
            for (int i = 0; i < curText.Length; i++)
            {
                if (char.IsDigit(curText[i]))
                {
                    chr += curText[i];
                }
            }
            inputField.text = chr;
        }
        
    }
    public void Reset()
    {
        inputField.text = "";
    }
}
