using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Util
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnChangeBankAccountMoney;


    [Header("User Data")]
    UserData userData;
    public Text userName;
    public Text myWalletMoneyText;
    public Text bankAccountMoneyText;

    [Header("BTN")]
    public GameObject btn;


    [Header("Deposit")]
    public GameObject depositUI;

    [Header("Withdrawal")]
    public GameObject withdrawalUI;

    [Header("Message")]
    public GameObject insufficientBalanceMessage;


    private void Start()
    {
        userData = DataManager.Instance.user;
        SetUserNameText();
        SetMyWalletText();
        SetBankAccountMoneyText();

        OnChangeBankAccountMoney += SetBankAccountMoneyText;
        OnChangeBankAccountMoney += SetMyWalletText;
    }

    public void CallOnChangeBankAccountMoney()
    {
        OnChangeBankAccountMoney?.Invoke();
    }




    void SetUserNameText()
    {
        userName.text = userData.userName;
    }
    void SetBankAccountMoneyText()
    {
        if(userData.bankAccount == 0)
        {
            bankAccountMoneyText.text = "0";
        }
        else
        {
            bankAccountMoneyText.text = GetThousandCommaText(userData.bankAccount);
        }
    }
    void SetMyWalletText()
    {
        if (userData.myWallet == 0)
        {
            myWalletMoneyText.text = "0";
        }
        else
        {
             myWalletMoneyText.text = GetThousandCommaText(userData.myWallet);
        }
    }









    public void ToggleDepositUI()
    {
        if (depositUI.activeSelf)
        {
            depositUI.SetActive(false);
            btn.SetActive(true);
        }
        else
        {
            btn.SetActive(false);
            depositUI.SetActive(true);
        }
    }
    public void ToggleWithdrawalUI()
    {
        if (withdrawalUI.activeSelf)
        {
            withdrawalUI.SetActive(false);
            btn.SetActive(true);
        }
        else
        {
            btn.SetActive(false);
            withdrawalUI.SetActive(true);
        }
    }

    public void ShowInsufficientBalanceMessage()
    {
        insufficientBalanceMessage.SetActive(true);
    }
    public void CloseBtn()
    {
        insufficientBalanceMessage.SetActive(false);
    }



}
